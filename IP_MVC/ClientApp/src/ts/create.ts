document.addEventListener('DOMContentLoaded', function () {
    const popupButton = document.getElementById('popupButton')!;

    const popupOverlay = document.getElementById('popupOverlay')!;
    
    const closePopup = document.getElementById('closePopup')!;

    const submitButton = document.getElementById('createButton')!;

    // Function to open the popup

    function openPopup() {

        popupOverlay.style.display = 'block';

    }

    // Function to close the popup

    function closePopupFunc() {

        popupOverlay.style.display = 'none';

    }

    // Function to submit the signup form

    // Event listeners

    // Trigger the popup to open (you can call this function on a button click or any other event)

    popupButton.addEventListener('click', openPopup);

    // Close the popup when the close button is clicked

    closePopup.addEventListener('click', closePopupFunc);
    
    submitButton.addEventListener('click', closePopupFunc);

    // Close the popup when clicking outside the popup content

    popupOverlay.addEventListener('click', function (event) {

        if (event.target === popupOverlay) {

            closePopupFunc();

        }

    });

    // You can customize and expand these functions based on your specific requirements.

});