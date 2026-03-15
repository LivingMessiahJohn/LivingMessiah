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

	// Check if click happened on (or inside) a link in the search modal
	const searchLink = event.target.closest('#menuNavbarSearchModal a');
	if (searchLink) {
		const modalEl = document.getElementById('menuNavbarSearchModal');
		if (modalEl) {
			const modalInstance = bootstrap.Modal.getInstance(modalEl);
			if (modalInstance) {
				modalInstance.hide();
			}
		}
	}
});

// Handles global keyboard shortcuts related to search.
window.searchHotkeys = {
	init: function () {
		document.addEventListener('keydown', function (e) {
			// Ctrl+K (or Cmd+K on macOS) opens the search modal
			if ((e.ctrlKey || e.metaKey) && (e.key === 'k' || e.key === 'K')) {
				e.preventDefault();
				const modalEl = document.getElementById('menuNavbarSearchModal');
				if (!modalEl) {
					return;
				}
				const modalInstance = bootstrap.Modal.getOrCreateInstance(modalEl);
				modalInstance.show();
			}
		});
	}
};

window.modalFocus = {
	registerFocus: function (modalId, element) {

		const modal = document.getElementById(modalId);

		modal.addEventListener('shown.bs.modal', function () {
			element.focus();
		});

	}
};

