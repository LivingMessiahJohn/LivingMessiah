window.connectionStatus = {
  isOnline: () => {
    return navigator.onLine ?? true;
  },

  registerEvents: (dotNetHelper) => {
    const updateStatus = () => {
      try {
        dotNetHelper.invokeMethodAsync('OnConnectionChanged', navigator.onLine ?? true);
      } catch {
        // DotNet helper may already be disposed.
      }
    };

    window.addEventListener('online', updateStatus);
    window.addEventListener('offline', updateStatus);

    // Return an object (not a bare function) so Blazor can InvokeVoidAsync("dispose").
    return {
      dispose: () => {
        window.removeEventListener('online', updateStatus);
        window.removeEventListener('offline', updateStatus);
        try {
          dotNetHelper.dispose();
        } catch {
          // already disposed
        }
      }
    };
  }
};
