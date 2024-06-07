let countdown: number = 10;
let isPaused: boolean = false;
const countdownElement: HTMLElement | null = document.getElementById('countdown');
const pauseButton : HTMLElement | null = document.getElementById('pauseButton');
const popupOverlay : HTMLElement | null = document.getElementById('pauseScreen');
const closeNoteButton : HTMLElement | null = document.getElementById('closeNote');
const submitNoteButton : HTMLElement | null = document.getElementById('submitNote');

function updateCountdown(): void {
    if (countdownElement) {
        countdownElement.innerText = countdown.toString();
        if (!isPaused)
            countdown--;
        if (countdown < 0) {
            const myForm: HTMLFormElement | null = document.getElementById('myForm') as HTMLFormElement;
            if (myForm) {
                myForm.setAttribute('action', '/Flow/SaveAnswerAndRedirect');
                const redirectedQuestionIdInput = document.getElementById("redirectedQuestionId") as HTMLInputElement;
                if (redirectedQuestionIdInput) {
                    redirectedQuestionIdInput.value = (parseInt(redirectedQuestionIdInput.value) + 1).toString();
                }
                myForm.submit();
            }
        } else {
            setTimeout(updateCountdown, 1000);
        }
    }
}

function openPopup(): void {
    if (popupOverlay) {
        popupOverlay.style.display = 'block';
        isPaused = true;
    }
}

function closePopup(): void {
    if (popupOverlay) {
        popupOverlay.style.display = 'none';
        isPaused = false;
    }
}

if (pauseButton) {
    pauseButton.addEventListener("click", openPopup);
}

if (closeNoteButton) {
    closeNoteButton.addEventListener("click", closePopup);
}

if (submitNoteButton) {
    submitNoteButton.addEventListener("click", closePopup);
}

updateCountdown();