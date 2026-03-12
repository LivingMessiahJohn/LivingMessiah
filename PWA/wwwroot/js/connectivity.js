window.connectionStatus = {
  isOnline: () => {
    return navigator.onLine ?? true;
  },

  registerEvents: (dotNetHelper) => {
    const updateStatus = () => {
      dotNetHelper.invokeMethodAsync('OnConnectionChanged', navigator.onLine ?? true);
    };

    window.addEventListener('online', updateStatus);
    window.addEventListener('offline', updateStatus);

    // Return cleanup function
    return () => {
      window.removeEventListener('online', updateStatus);
      window.removeEventListener('offline', updateStatus);
    };
  }
};