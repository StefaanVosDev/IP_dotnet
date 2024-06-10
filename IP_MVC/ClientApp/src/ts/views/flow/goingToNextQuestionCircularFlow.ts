import { showPopup } from "../../showPopUp";

let countdown: number = 30;
let isPaused: boolean = false;
const countdownElement: HTMLElement | null = document.getElementById('countdown');
const popupButton: HTMLElement | null = document.getElementById('popupButton');
const closeNoteButton: HTMLElement | null = document.getElementById('closeNote');
const closePopupButton: HTMLElement | null = document.getElementById('closePopup');

let timeoutId: number | null = null;

function updateCountdown(): void {
    if (countdownElement) {
        countdownElement.innerText = countdown.toString();
        if (!isPaused) {
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
                timeoutId = window.setTimeout(updateCountdown, 1000);
            }
        }
    }
}

function pauseCountdown(): void {
    isPaused = true;
    if (timeoutId !== null) {
        clearTimeout(timeoutId);
        timeoutId = null;
    }
}

function resumeCountdown(): void {
    isPaused = false;
    if (timeoutId === null) {
        updateCountdown();
    }
}

if (popupButton) {
    showPopup(true);
    popupButton.addEventListener('click', pauseCountdown);
}

if (closeNoteButton) {
    closeNoteButton.addEventListener('click', function (event) {
        showPopup(false);
        resumeCountdown();
    });
}

if (closePopupButton) {
    closePopupButton.addEventListener('click', resumeCountdown);
}

// Start the initial countdown
updateCountdown();
