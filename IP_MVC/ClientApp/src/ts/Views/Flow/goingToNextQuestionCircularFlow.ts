let countdown: number = 8;
const countdownElement: HTMLElement | null = document.getElementById('countdown');

function updateCountdown(): void {
    if (countdownElement) {
        countdownElement.innerText = countdown.toString();
        countdown--;
        if (countdown < 0) {
            // Perform action after countdown
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

updateCountdown();