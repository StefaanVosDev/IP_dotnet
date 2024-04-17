window.addEventListener('DOMContentLoaded', () => {  //Selecting default value, when page is loaded (not possible trough bootstrap slider)
    const selectedValue = document.getElementById('selectedValue') as HTMLInputElement;
    selectedValue.value = String((window as any).Model.Min); // Assuming @Model.Min is defined globally
});

document.querySelector('input[type="range"]').addEventListener('input', (e) => {
    const selectedValue = document.getElementById('selectedValue') as HTMLInputElement;
    selectedValue.value = (e.target as HTMLInputElement).value;
});