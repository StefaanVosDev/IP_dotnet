document.addEventListener('DOMContentLoaded', function () {
    
    //Create popup
    const popupCreateButton = document.getElementById('popupCreateButton')!;
    const popupCreateOverlay = document.getElementById('popupCreateOverlay')!;
    const closeCreatePopup = document.getElementById('closeCreatePopup')!;
    const submitCreateButton = document.getElementById('createButton')!;

    function openCreatePopup() {
        popupCreateOverlay.style.display = 'block';
    }


    function closeCreatePopupFunc() {
        popupCreateOverlay.style.display = 'none';
    }


    popupCreateButton.addEventListener('click', openCreatePopup);
    closeCreatePopup.addEventListener('click', closeCreatePopupFunc);
    submitCreateButton.addEventListener('click', closeCreatePopupFunc);
    popupCreateOverlay.addEventListener('click', function (event) {

        if (event.target === popupCreateOverlay) {
            closeCreatePopupFunc();
        }

    });

});