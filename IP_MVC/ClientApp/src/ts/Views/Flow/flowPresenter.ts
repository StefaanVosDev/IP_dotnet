import * as client from "./restFlowClient"

const editButtons = document.querySelectorAll('.edit-btn');

async function showFlow(id: string, projectCard: HTMLElement) {
    try {
        const project = await client.getFlow(id);
        const cardBody = projectCard.querySelector('.front')!;

        cardBody.innerHTML = `
                            <h5 class="card-title">${project.name}</h5>
                            <p class="card-text">${project.description}</p>`;
    } catch (e) {
        console.error('Error showing flow: ', e);
    }

}

async function changeProject(flowCard: HTMLElement) {
    const nameInput = flowCard.querySelector('#nameInput') as HTMLInputElement;
    const descriptionInput = flowCard.querySelector('#descriptionInput') as HTMLInputElement;
    const projectIdInput = flowCard.querySelector('#id') as HTMLInputElement;

    try {
        await client.updateFlow(nameInput.value, descriptionInput.value, projectIdInput.value);
    } catch (error) {
        console.error('Error updating flow:', error);
        alert('There was an issue updating the flow. Please try again.');
    }

    await showFlow(projectIdInput.value, flowCard);

    const cardInner = flowCard.querySelector('.flip-card-inner');
    if (cardInner) {
        cardInner.classList.toggle('flipped');
    }
}

function editFlow(editButton: Element) {
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
        editFlow(button)
    )
});