document.addEventListener('DOMContentLoaded', function () {
    const popupButton = document.getElementById('popupButton')!;
    const popupOverlay = document.getElementById('popupOverlay')!;
    const closePopup = document.getElementById('closePopup')!;
    const submitButton = document.getElementById('createButton')!;

    function openPopup() {
        popupOverlay.style.display = 'block';
    }


    function closePopupFunc() {
        popupOverlay.style.display = 'none';
    }
    

    popupButton.addEventListener('click', openPopup);
    closePopup.addEventListener('click', closePopupFunc);
    submitButton.addEventListener('click', closePopupFunc);
    popupOverlay.addEventListener('click', function (event) {

        if (event.target === popupOverlay) {
            closePopupFunc();
        }

    });
    
});