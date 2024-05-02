document.addEventListener("DOMContentLoaded", function () {
    const clickableCards = document.querySelectorAll('.card-clickable');

    clickableCards.forEach(function (card) {
        card.addEventListener("click", function () {
            const parentFlowId = card.getAttribute('data-parent-flow-id');

            if (parentFlowId !== null) {
                window.location.href = `/Project/Inzoom/${parentFlowId}`;
            } else {
                console.error("ParentFlowId is null. Cannot generate URL.");
            }
        });
    });
});