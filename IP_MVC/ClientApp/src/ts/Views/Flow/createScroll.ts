import Swiper from 'swiper';

// Maak een globale variabele voor de Swiper-instantie
let swiper: Swiper;

// Functie om Swiper-instantie bij te werken
export function updateSwiper() {
    swiper.update();
}

const nextButton = document.querySelector(".swiper-button-next");
const prevButton = document.querySelector(".swiper-button-prev");

if (nextButton && prevButton) {
    nextButton.addEventListener("click", function () {
        swiper.slideNext();
    });
    prevButton.addEventListener("click", function () {
        swiper.slidePrev();
    });
}

// Functie om Swiper-instantie te initialiseren
function initSwiper() {
    swiper = new Swiper(".swiper", {
        slidesPerView: 1,
        loop: true,
        centeredSlides: true,
        spaceBetween: 10,

        pagination: {
            el: ".swiper-pagination",
            clickable: true
        },

        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        breakpoints: {
            320: {
                slidesPerView: 1,
                spaceBetween: 20
            },
            480: {
                slidesPerView: 1,
                spaceBetween: 30
            },
            640: {
                slidesPerView: 1,
                spaceBetween: 40
            }
        }
    });
}

initSwiper();