import Swiper from 'swiper';

document.addEventListener("DOMContentLoaded", function() {
    const swiper = new Swiper(".swiper", {
        // Default parameters
        slidesPerView: 1,
        loop: true,
        centeredSlides: true,
        spaceBetween: 10,
        // Navigation arrows
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        // Responsive breakpoints
        breakpoints: {
            // when window width is >= 320px
            320: {
                slidesPerView: 1,
                spaceBetween: 20
            },
            // when window width is >= 480px
            480: {
                slidesPerView: 1,
                spaceBetween: 30
            },
            // when window width is >= 640px
            640: {
                slidesPerView: 1,
                spaceBetween: 40
            }
        }
    });

    // Eventlistener voor de volgende knop
    const nextButton = document.querySelector(".swiper-button-next");
    if (nextButton) {
        nextButton.addEventListener("click", function() {
            swiper.slideNext(); // Ga naar de volgende dia
        });
    }

    // Eventlistener voor de vorige knop
    const prevButton = document.querySelector(".swiper-button-prev");
    if (prevButton) {
        prevButton.addEventListener("click", function() {
            swiper.slidePrev(); // Ga naar de vorige dia
        });
    }
});
