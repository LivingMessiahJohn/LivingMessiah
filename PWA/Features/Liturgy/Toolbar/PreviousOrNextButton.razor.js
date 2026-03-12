export function addArrowKeyListener(dotNetObject) {
	document.addEventListener('keydown', (event) => {
		if (event.key === 'ArrowLeft' || event.key === 'ArrowRight') {
			dotNetObject.invokeMethodAsync('HandleArrowKey', event.key);
		}
	});
}

export function removeArrowKeyListener() {
	document.removeEventListener('keydown');
}
