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
    
    //Edit popup
    const popupEditButton = document.getElementById('popupEditButton')!;
    const popupEditOverlay = document.getElementById('popupEditOverlay')!;
    const closeEditPopup = document.getElementById('closeEditPopup')!;
    const submitEditButton = document.getElementById('createButton')!;

    function openEditPopup() {
        popupEditOverlay.style.display = 'block';
    }


    function closeEditPopupFunc() {
        popupEditOverlay.style.display = 'none';
    }


    popupEditButton.addEventListener('click', openEditPopup);
    closeEditPopup.addEventListener('click', closeEditPopupFunc);
    submitEditButton.addEventListener('click', closeEditPopupFunc);
    popupEditOverlay.addEventListener('click', function (event) {

        if (event.target === popupEditOverlay) {
            closeEditPopupFunc();
        }

    });

});