// wwwroot/theme.js
(function () {
  const STORAGE_KEY = 'preferred-theme';
  const html = document.documentElement;

  function getTheme() {
    const saved = localStorage.getItem(STORAGE_KEY);
    if (saved) return saved; // 'light' | 'dark' | 'auto'

    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
  }

  function applyTheme() {
    let mode = localStorage.getItem(STORAGE_KEY) || 'auto';
    let effective = (mode === 'auto')
      ? (window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light')
      : mode;

    html.setAttribute('data-bs-theme', effective);
  }

  // Run immediately (prevents flash)
  applyTheme();

  // React to system changes when in 'auto'
  window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
    if ((localStorage.getItem(STORAGE_KEY) || 'auto') === 'auto') {
      applyTheme();
    }
  });

  // Exposed for Blazor
  window.theme = {
    get: () => localStorage.getItem(STORAGE_KEY) || 'auto',
    set: (mode) => {
      localStorage.setItem(STORAGE_KEY, mode);
      applyTheme();
    }
  };
})();