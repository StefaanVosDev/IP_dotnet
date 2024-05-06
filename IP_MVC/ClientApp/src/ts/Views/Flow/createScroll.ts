import Swiper from 'swiper';

document.addEventListener("DOMContentLoaded", function() {
    const swiper = new Swiper(".swiper", {
        slidesPerView: 1,
        loop: true,
        centeredSlides: true,
        spaceBetween: 10,

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

    const nextButton = document.querySelector(".swiper-button-next");
    if (nextButton) {
        nextButton.addEventListener("click", function() {
            swiper.slideNext(); // Ga naar de volgende dia
        });
    }

    const prevButton = document.querySelector(".swiper-button-prev");
    if (prevButton) {
        prevButton.addEventListener("click", function() {
            swiper.slidePrev(); // Ga naar de vorige dia
        });
    }
});
