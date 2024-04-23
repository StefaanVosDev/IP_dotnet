window.addEventListener('DOMContentLoaded', function () {
    var selectedValue = document.getElementById('selectedValue');
    selectedValue.value = selectedValue.dataset.earlierAnswer;
});

document.querySelector('input[type="range"]').addEventListener('input', function (e) {
    var selectedValue = document.getElementById('selectedValue');
    selectedValue.value = e.target.value;
});