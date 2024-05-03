document.addEventListener("DOMContentLoaded", function () {
    var cards = document.querySelectorAll('.swipe');
    for (var i = 1; i < cards.length; i++) {
        cards[i].classList.add('hidden');
    }
});

var currentIndex = 0;
var cards = document.querySelectorAll('.swipe');

function nextCard() {
    cards[currentIndex].classList.add('hidden');
    currentIndex = (currentIndex + 1) % cards.length;
    cards[currentIndex].classList.remove('hidden');
}