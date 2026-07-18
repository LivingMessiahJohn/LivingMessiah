// Caution! Be sure you understand the caveats before publishing an application with
// offline support. See https://aka.ms/blazor-offline-considerations

self.importScripts('./service-worker-assets.js');
self.addEventListener('install', event => event.waitUntil(onInstall(event)));
self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));
self.addEventListener('message', event => { if (event.data?.type === 'SKIP_WAITING') self.skipWaiting(); });

const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;
const offlineAssetsInclude = [ /\.dll$/, /\.pdb$/, /\.wasm/, /\.html$/, /\.js$/, /\.json$/, /\.css$/, /\.woff$/, /\.png$/, /\.jpe?g$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/, /\.svg$/, /\.webmanifest$/ ];
const offlineAssetsExclude = [ /^service-worker\.js$/ ];

// Replace with your base path if you are hosting on a subfolder. Ensure there is a trailing '/'.
const base = "/";
const baseUrl = new URL(base, self.origin);
const manifestUrlList = self.assetsManifest.assets.map(asset => new URL(asset.url, baseUrl).href);

async function onInstall(event) {
    console.info('Service worker: Install');

    // Fetch and cache all matching items from the assets manifest
    const assetsRequests = self.assetsManifest.assets
        .filter(asset => offlineAssetsInclude.some(pattern => pattern.test(asset.url)))
        .filter(asset => !offlineAssetsExclude.some(pattern => pattern.test(asset.url)))
        .map(asset => new Request(asset.url, { integrity: asset.hash, cache: 'no-cache' }));
    await caches.open(cacheName).then(cache => cache.addAll(assetsRequests));
}

async function onActivate(event) {
    console.info('Service worker: Activate');

    // Delete unused caches
    const cacheKeys = await caches.keys();
    await Promise.all(cacheKeys
        .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
        .map(key => caches.delete(key)));
}

async function onFetch(event) {
    // Always hit the network for API calls (never serve stale/cached API responses).
    const url = new URL(event.request.url);
    if (url.pathname.startsWith('/api/')) {
        return fetch(event.request);
    }

    if (event.request.method !== 'GET') {
        return fetch(event.request);
    }

    // Navigations: network-first when online so installed PWAs pick up new deploys.
    // Fall back to cached index.html when offline.
    const shouldServeIndexHtml = event.request.mode === 'navigate'
        && !manifestUrlList.some(assetUrl => assetUrl === event.request.url);

    if (shouldServeIndexHtml) {
        try {
            const networkResponse = await fetch(event.request);
            if (networkResponse && networkResponse.ok) {
                const cache = await caches.open(cacheName);
                // Keep offline shell fresh for the next offline open.
                cache.put('index.html', networkResponse.clone());
                return networkResponse;
            }
        } catch {
            // Offline or network failure — use cache below.
        }

        const cache = await caches.open(cacheName);
        const cachedIndex = await cache.match('index.html');
        if (cachedIndex) {
            return cachedIndex;
        }

        // Last resort: try network again (may still fail offline).
        return fetch(event.request);
    }

    // Static assets: cache-first, then network.
    const cache = await caches.open(cacheName);
    const cachedResponse = await cache.match(event.request);
    return cachedResponse || fetch(event.request);
}
