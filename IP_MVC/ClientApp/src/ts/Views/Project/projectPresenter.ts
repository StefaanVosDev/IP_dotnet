import * as client from "./restProjectClient"

const editButtons = document.querySelectorAll('.edit-btn');

async function changeProject(projectCard: Element) {
    const nameInput = projectCard.querySelector('#nameInput') as HTMLInputElement;
    const descriptionInput = projectCard.querySelector('#descriptionInput') as HTMLInputElement;
    const projectIdInput = projectCard.querySelector('#id') as HTMLInputElement;
    const adminIdInput = projectCard.querySelector('#adminId') as HTMLInputElement;

    const updatedProject = await client.updateProject(nameInput.value, descriptionInput.value, projectIdInput.value, adminIdInput.value);

    const cardBody = projectCard.querySelector('.front')!;

    cardBody.innerHTML = `
    <h5 class="card-title">${updatedProject.name}</h5>
    <p class="card-text">${updatedProject.descriptionn}</p>`;


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

            const updateProjectButton = cardContainer.querySelector("#updateProjectButton")!;
            if (updateProjectButton) {
                addUpdateButtonListener(cardContainer, updateProjectButton);
            } else {
                console.error("updateProjectButton not found");
            }

            // setTimeout(() => {
            //    const updateProjectButton = cardContainer.querySelector("#updateProjectButton");
            //    if (updateProjectButton) {
            //       addUpdateButtonListener(cardContainer, updateProjectButton);
            //    } else {
            //       console.error("updateProjectButton not found");
            //    }
            // }, 100);
        }
    }
}

editButtons.forEach(button => {
    button.addEventListener('click', event => {
        editProject(button)
    })
});

