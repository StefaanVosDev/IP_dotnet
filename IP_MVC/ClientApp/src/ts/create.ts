document.addEventListener('DOMContentLoaded', function () {
    const popupButton = document.getElementById('popupButton')!;

    function showPopup() {
        const popupOverlay = document.getElementById('popupOverlay')!;
        const popup = document.getElementById('popup')!;
        const closePopup = document.getElementById('closePopup')!;
        const emailInput = document.getElementById('emailInput') as HTMLInputElement;
        const submitButton = document.getElementById('signUpButton')!;

        // Function to open the popup

        console.log("test");

        function openPopup() {

            popupOverlay.style.display = 'block';

        }

        // Function to close the popup

        function closePopupFunc() {

            popupOverlay.style.display = 'none';

        }

        // Trigger the popup to open (you can call this function on a button click or any other event)

        openPopup();

        submitButton.addEventListener('click', closePopupFunc);

        // Close the popup when the close button is clicked

        closePopup.addEventListener('click', closePopupFunc);

        // Close the popup when clicking outside the popup content

        popupOverlay.addEventListener('click', function (event) {
            if (event.target === popupOverlay) {
                closePopupFunc();
            }
        });

        // You can customize and expand these functions based on your specific requirements.
    }

    popupButton.addEventListener('click', showPopup);
});