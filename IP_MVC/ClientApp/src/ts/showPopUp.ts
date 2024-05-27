const popupButton = document.getElementById('popupButton')!;
const popupOverlayBody = document.getElementById('popupOverlay')!;
const closePopupButton = document.getElementById('closePopup')!;
const submitButton = document.getElementById('createButton')!;

function showPopup(open: boolean) {
    if (open){
        popupOverlayBody.style.display = 'block';
    } else {
        popupOverlayBody.style.display = 'none';
    }
}

popupButton.addEventListener('click', () => showPopup(true));
closePopupButton.addEventListener('click', () => showPopup(false));
submitButton.addEventListener('click', () => showPopup(false));
popupOverlayBody.addEventListener('click', function (event) {
    if (event.target === popupOverlay) {
        showPopup(false);
    }
}); 
