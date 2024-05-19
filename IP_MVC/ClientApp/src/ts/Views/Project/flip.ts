document.addEventListener('DOMContentLoaded', () => {
    const editButtons = document.querySelectorAll('.edit-btn');

    editButtons.forEach(button => {
        button.addEventListener('click', () => {
            const cardContainer = button.closest('.col-sm-12');
            if (cardContainer) {
                const cardInner = cardContainer.querySelector('.flip-card-inner');
                if (cardInner) {
                    cardInner.classList.toggle('flipped');
                }
            }
        });
    });
});