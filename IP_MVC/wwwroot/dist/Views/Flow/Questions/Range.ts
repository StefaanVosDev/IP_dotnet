window.addEventListener('DOMContentLoaded', () => {  //Selecting default value, when page is loaded (not possible trough bootstrap slider)    
    const selectedValue = document.getElementById('selectedValue') as HTMLInputElement;
    selectedValue.value = selectedValue.dataset.min;
});

document.querySelector('input[type="range"]').addEventListener('input', (e) => {
    const selectedValue = document.getElementById('selectedValue') as HTMLInputElement;
    selectedValue.value = (e.target as HTMLInputElement).value;
});