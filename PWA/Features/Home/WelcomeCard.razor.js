// WelcomeCard.razor.js
const STORAGE_KEY = 'welcome-card-showing-details';

export function getShowingDetails() {
  const saved = localStorage.getItem(STORAGE_KEY);
  if (saved !== null) {
    return saved === 'true';
  }
  return true; // Default to true
}

export function setShowingDetails(value) {
  localStorage.setItem(STORAGE_KEY, value.toString());
}
