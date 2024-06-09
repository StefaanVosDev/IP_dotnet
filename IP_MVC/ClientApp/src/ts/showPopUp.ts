import {end} from "@popperjs/core";

const popupButton = document.getElementById('popupButton')!;
const popupOverlayBody = document.getElementById('popupOverlay')!;
const closePopupButton = document.getElementById('closePopup')!;
const submitButton = document.getElementById('createButton')!;
const startDate = document.getElementById('NewStartDateInput') as HTMLInputElement;
const endDate = document.getElementById('NewEndDateInput')as HTMLInputElement;
function showPopup(open: boolean) {
    if (open){
        popupOverlayBody.style.display = 'block';
        if (startDate != null && endDate != null){
            const currentDate = new Date().toISOString().slice(0, 16); 
            startDate.value = currentDate;
            endDate.value = currentDate;
        }
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