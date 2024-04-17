var countdown = 8;
var countdownElement = document.getElementById('countdown');
function updateCountdown() {
    if (countdownElement) {
        countdownElement.innerText = countdown.toString();
        countdown--;
        if (countdown < 0) {
            // Perform action after countdown
            var myForm = document.getElementById('myForm');
            if (myForm) {
                myForm.setAttribute('action', '/Flow/SaveAndNext');
                myForm.submit();
            }
        }
        else {
            setTimeout(updateCountdown, 1000);
        }
    }
}
updateCountdown();