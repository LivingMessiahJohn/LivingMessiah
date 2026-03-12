let dotNetHelper = null;

export function initializeScrollToTop(helper) {
  dotNetHelper = helper;

  const scrollButton = document.querySelector('.scroll-to-top');

  window.addEventListener('scroll', () => {
    const shouldShow = window.scrollY > 300; // show after 300px scroll
    dotNetHelper.invokeMethodAsync('ShowButton', shouldShow);
  });
}