import * as client from "./restFlowClient"

const editButtons = document.querySelectorAll('.edit-flow-btn');
const backButton = document.querySelectorAll('.btn-back');

async function showFlow(id: string, projectCard: HTMLElement) {
    try {
        const flow = await client.getFlow(id);
        const cardBody = projectCard.querySelector('.front')!;

        cardBody.innerHTML = `
                            <h5 class="card-title">${flow.name}</h5>
                            <p class="card-text">${flow.description}</p>`;
    } catch (e) {
        console.error('Error showing project: ', e);
    }

}

async function changeFlow(flowCard: HTMLElement) {
    const nameInput = flowCard.querySelector('#nameInput') as HTMLInputElement;
    const descriptionInput = flowCard.querySelector('#descriptionInput') as HTMLInputElement;
    const flowIdInput = flowCard.querySelector('#flowId') as HTMLInputElement;
    try {
        await client.updateFlow(nameInput.value, descriptionInput.value, flowIdInput.value);
    } catch (error) {
        console.error('Error updating flow:', error);
        alert('There was an issue updating the flow. Please try again.');
    }

    await showFlow(flowIdInput.value, flowCard);

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

            const updateFlowButton = cardContainer.querySelector("#updateFlowButton");
            if (updateFlowButton) {
                updateFlowButton.addEventListener('click', async () => changeFlow(cardContainer), {once: true});
            } else {
                console.error("updateFlowButton not found");
            }
        }
    }
}

editButtons.forEach(button => {
    button.addEventListener('click', () =>
        editFlow(button)
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