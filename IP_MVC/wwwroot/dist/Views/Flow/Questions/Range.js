window.addEventListener('DOMContentLoaded', function () {
    var selectedValue = document.getElementById('selectedValue');
    selectedValue.value = String(window.Model.Min); // Assuming @Model.Min is defined globally
});
document.querySelector('input[type="range"]').addEventListener('input', function (e) {
    var selectedValue = document.getElementById('selectedValue');
    selectedValue.value = e.target.value;
});
