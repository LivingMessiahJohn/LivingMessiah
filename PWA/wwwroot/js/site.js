window.DomIds = {
  offCanvas: 'offCanvas'
};

window.initializeDomIds = function (offCanvas, navbar) {
  window.DomIds.offCanvas = offCanvas;
};

document.addEventListener('click', function (event) {
  // Check if click happened on (or inside) a .nav-link in this offcanvas
  const link = event.target.closest(`#${window.DomIds.offCanvas} .nav-link`);
  if (link) {
    const offcanvasEl = document.getElementById(window.DomIds.offCanvas);
    if (offcanvasEl) {
      const offcanvasInstance = bootstrap.Offcanvas.getInstance(offcanvasEl);
      if (offcanvasInstance) {
        offcanvasInstance.hide();
      }
    }
  }
});
