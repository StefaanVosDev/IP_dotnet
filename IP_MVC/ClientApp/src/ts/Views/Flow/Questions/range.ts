window.addEventListener('DOMContentLoaded', () => {
    const selectedValue = document.getElementById('selectedValue') as HTMLInputElement;
    selectedValue.value = selectedValue.dataset.earlierAnswer || '';
});

document.querySelector('input[type="range"]')?.addEventListener('input', (e) => {
    const selectedValue = document.getElementById('selectedValue') as HTMLInputElement;
    selectedValue.value = (e.target as HTMLInputElement).value;
});