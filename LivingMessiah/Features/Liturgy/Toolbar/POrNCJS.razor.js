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

/*
let dotNetRef = null;

export function registerKeyboard(dotNetHelper) {
	dotNetRef = dotNetHelper;
	window.addEventListener('keydown', handleKeyDown);
}

export function disposeKeyboard() {
	window.removeEventListener('keydown', handleKeyDown);
	dotNetRef = null;
}

function handleKeyDown(event) {
	if (!dotNetRef) return;
	const key = event.key;
	dotNetRef.invokeMethodAsync('HandleArrowKey', key);
}
*/

