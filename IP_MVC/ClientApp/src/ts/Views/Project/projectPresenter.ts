import * as client from "./restProjectClient"

const editButtons = document.querySelectorAll('.edit-btn');
const backButton = document.querySelectorAll('.btn-back');
const linearButton = document.querySelectorAll("#linearButton");
const circularButton = document.querySelectorAll("#circularButton");
const backCardButton = document.querySelectorAll("#backButton");
const startSessie = document.querySelectorAll(".startSessie");

async function showProject(id: string, projectCard: HTMLElement) {
    try {
        const project = await client.getProject(id);
        const cardBody = projectCard.querySelector('.front')!;

        cardBody.innerHTML = `
                            <h5 class="card-title">${project.name}</h5>
                            <p class="card-text">${project.description}</p>`;
    } catch (e) {
        console.error('Error showing project: ', e);
    }

}

async function changeProject(projectCard: HTMLElement) {
    const nameInput = projectCard.querySelector('#nameInput') as HTMLInputElement;
    const descriptionInput = projectCard.querySelector('#descriptionInput') as HTMLInputElement;
    const projectIdInput = projectCard.querySelector('#id') as HTMLInputElement;
    const adminIdInput = projectCard.querySelector('#adminId') as HTMLInputElement;

    try {
        await client.updateProject(nameInput.value, descriptionInput.value, projectIdInput.value, adminIdInput.value);
    } catch (error) {
        console.error('Error updating project:', error);
        alert('There was an issue updating the project. Please try again.');
    }

    await showProject(projectIdInput.value, projectCard);

    const cardInner = projectCard.querySelector('.flip-card-inner');
    if (cardInner) {
        cardInner.classList.toggle('flipped');
    }
}

function editProject(editButton: Element) {
    const cardContainer = editButton.closest('.flip-card') as HTMLBodyElement;
    if (cardContainer) {
        const cardInner = cardContainer.querySelector('.flip-card-inner');
        if (cardInner) {
            cardInner.classList.toggle('flipped');

            const updateProjectButton = cardContainer.querySelector("#updateProjectButton");
            if (updateProjectButton) {
                updateProjectButton.addEventListener('click', async () => changeProject(cardContainer), {once: true});
            } else {
                console.error("updateProjectButton not found");
            }
        }
    }
}

editButtons.forEach(button => {
    button.addEventListener('click', () =>
        editProject(button)
    )
});

backButton.forEach(button => {
    button.addEventListener('click', () => {
        const cardContainer = button.closest('.flip-card') as HTMLBodyElement;
        if (cardContainer) {
            const cardInner = cardContainer.querySelector('.flip-card-inner');
            if (cardInner) {
                cardInner.classList.toggle('flipped');
            }
        }
    })
});

startSessie.forEach(button => {
    button.addEventListener("click", function (event) {
        event.preventDefault();

        const project = button.closest(".flip-card-front")?.querySelector(".show-project");
        if (project) {
            project.classList.remove("d-block")
            project.classList.add("d-none")
        }

        const warning = button.closest(".flip-card-front")?.querySelector(".show-warning");
        if (warning) {
            warning.classList.remove("d-none")
            warning.classList.add("d-block")
        }
    });
});

linearButton.forEach(button => {
    button.addEventListener("click", function () {
        redirectToFlow(button as HTMLElement, false)    });
});

circularButton.forEach(button => {
    button.addEventListener("click", function () {
        redirectToFlow(button as HTMLElement, true)    
    });
});

backCardButton.forEach(button => {
    button.addEventListener("click", function () {
        showProjectCard(button)
    });
});

function redirectToFlow(button: HTMLElement, isCircular: boolean) {
    const flowId = button.dataset.flowId;
    console.log(flowId);
    const circular = isCircular !== null ? isCircular : '';
    window.location.href = `/Flow/Flow?projectId=${flowId}&circular=${circular}`;
}
function showProjectCard(button: Element) {
        const project = button.closest(".flip-card-front")?.querySelector(".show-project");
        console.log(project);
        if (project) {
            project.classList.remove("d-none")
            project.classList.add("d-block")

            console.log(project);
        }

        const warning = button.closest(".flip-card-front")?.querySelector(".show-warning");
        if (warning) {
            warning.classList.remove("d-block")
            warning.classList.add("d-none")
        }
}