#!/usr/bin/env bash
# Build a relocatable Ghostscript tree under /export (Debian bookworm).
# Used by setup-ghostscript-mount.ps1 via Docker.
set -euo pipefail

export DEBIAN_FRONTEND=noninteractive
apt-get update -qq
apt-get install -y -qq ghostscript file ca-certificates findutils coreutils >/dev/null

echo "gs version: $(gs --version)"
GS_PATH="$(command -v gs)"
echo "gs path: $GS_PATH"
file "$GS_PATH"
REAL_GS="$(readlink -f "$GS_PATH" || true)"
if [[ -z "${REAL_GS}" ]]; then
  REAL_GS="$GS_PATH"
fi
echo "real: $REAL_GS"

rm -rf /export/*
mkdir -p /export/bin /export/lib /export/share

copy_deps() {
  local bin="$1"
  if [[ ! -f "$bin" ]]; then
    return 0
  fi
  if file "$bin" | grep -qi "ELF"; then
    # shellcheck disable=SC2013
    for lib in $(ldd "$bin" | awk '/=>/ {print $3} /^\// {print $1}'); do
      if [[ -n "$lib" && -f "$lib" ]]; then
        # Copy real file content (follow symlink) under the basename Azure Files will use
        cp -L -n "$lib" /export/lib/ 2>/dev/null || true
      fi
    done
    local base
    base="$(basename "$bin")"
    # Only keep ELF tools under bin/ (avoid dumping libgs into bin)
    if [[ "$base" == gs || "$base" == gs.bin || "$base" == ghostscript ]]; then
      cp -L -f "$bin" "/export/bin/$base"
      chmod +x "/export/bin/$base"
    fi
  fi
}

# -L: dereference symlinks so Windows hosts / Azure Files upload-batch can read files
if [[ -d /usr/share/ghostscript ]]; then
  cp -aL /usr/share/ghostscript /export/share/
fi

# Debian often ships /usr/bin/gs as a tiny wrapper; find the real ELF.
copy_deps "$REAL_GS"
copy_deps "$GS_PATH"

while IFS= read -r -d '' f; do
  if file "$f" | grep -qi ELF; then
    copy_deps "$f"
    cp -f "$f" /export/bin/gs.bin
    chmod +x /export/bin/gs.bin
  fi
done < <(find /usr -type f \( -name 'gs' -o -name 'gs.bin' \) -print0 2>/dev/null)

while IFS= read -r -d '' lib; do
  cp -L -n "$lib" /export/lib/ || true
  copy_deps "$lib"
done < <(find /usr/lib -name 'libgs.so*' -print0 2>/dev/null)

# Materialize any remaining symlinks under /export (Windows bind mounts)
find /export -type l -print0 2>/dev/null | while IFS= read -r -d '' link; do
  target="$(readlink -f "$link" || true)"
  if [[ -n "$target" && -f "$target" ]]; then
    rm -f "$link"
    cp -L "$target" "$link"
  elif [[ -n "$target" && -d "$target" ]]; then
    rm -f "$link"
    cp -aL "$target" "$link"
  fi
done

# Entrypoint used by the Function app
cat > /export/bin/gs << 'EOF'
#!/bin/sh
# Relocatable Ghostscript launcher for Azure Files mount.
ROOT="$(CDPATH= cd -- "$(dirname "$0")/.." && pwd)"
export LD_LIBRARY_PATH="$ROOT/lib${LD_LIBRARY_PATH:+:$LD_LIBRARY_PATH}"

if [ -d "$ROOT/share/ghostscript" ]; then
  VERDIR=$(ls -1d "$ROOT/share/ghostscript"/[0-9]* 2>/dev/null | head -1)
  if [ -n "$VERDIR" ]; then
    export GS_LIB="$VERDIR/Resource/Init:$VERDIR/Resource/Font:$VERDIR/iccprofiles${GS_LIB:+:$GS_LIB}"
    export GS_FONTPATH="$VERDIR/Resource/Font${GS_FONTPATH:+:$GS_FONTPATH}"
  fi
fi

if [ -x "$ROOT/bin/gs.bin" ]; then
  exec "$ROOT/bin/gs.bin" "$@"
fi

echo "Ghostscript ELF (gs.bin) missing under $ROOT/bin" >&2
exit 127
EOF
chmod +x /export/bin/gs

echo "=== export listing (first 60) ==="
# Avoid SIGPIPE/141 under pipefail when head closes early
set +o pipefail
find /export -type f | head -60 || true
echo "=== sizes ==="
du -sh /export /export/bin /export/lib /export/share 2>/dev/null || true
set -o pipefail

export LD_LIBRARY_PATH=/export/lib
# Dynamic linker for PIE binaries that reference absolute ld path
if [[ -f /lib64/ld-linux-x86-64.so.2 && ! -f /export/lib/ld-linux-x86-64.so.2 ]]; then
  cp -n /lib64/ld-linux-x86-64.so.2 /export/lib/ 2>/dev/null || true
fi

if [[ -x /export/bin/gs.bin ]]; then
  echo "=== smoke test gs.bin -v ==="
  /export/bin/gs.bin -v
  echo "=== smoke test wrapper ==="
  /export/bin/gs -v
else
  echo "ERROR: gs.bin not packaged" >&2
  exit 1
fi

echo "OK packaged Ghostscript into /export"
