let slideIndex: number = 0;

function plusDivs(n: number): void {
    showDivs(slideIndex += n);
}

function showDivs(n: number): void {
    let i: number;
    let x: HTMLCollectionOf<HTMLElement> = document.getElementsByClassName("flow-card") as HTMLCollectionOf<HTMLElement>;

    if (x.length === 0) {
        console.error("Geen elementen gevonden met de klasse naam 'flow-card'");
        return;
    }

    if (n > x.length) {
        slideIndex = 1;
    }
    if (n < 1) {
        slideIndex = x.length;
    }

    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }

    x[slideIndex - 1].style.display = "block";
}

document.addEventListener('click', function(event) {
    if (event.target) {
        if ((event.target as Element).matches('.w3-display-left')) {
            plusDivs(-1);
        } else if ((event.target as Element).matches('.w3-display-right')) {
            plusDivs(1);
        }
    }
});

window.onload = function () {
    showDivs(slideIndex);
};
