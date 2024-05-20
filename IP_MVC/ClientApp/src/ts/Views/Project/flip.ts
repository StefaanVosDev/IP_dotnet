async function updateProject(name: string, description: string, projectId: string, adminId: string) {
    try {
        const response = await fetch(`/api/Projects`, {
            method: 'PUT',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                ProjectId: projectId,
                AdminId: adminId,
                NewName: name,
                NewDescription: description
            }),
        });
        if (!response.ok) {
            throw new Error(`Failed to update project: ${response.statusText}`);
        }
        const result = await response.json();
        return result.result;
    } catch (error) {
        console.error('Error updating project:', error);
        alert('There was an issue updating the project. Please try again.');
    }
}
function addUpdateButtonListener(cardContainer: Element, updateProjectButton: Element) {
    updateProjectButton.addEventListener("click", async () => {
        const nameInput = cardContainer.querySelector('#nameInput') as HTMLInputElement;
        const descriptionInput = cardContainer.querySelector('#descriptionInput') as HTMLInputElement;
        const projectIdInput = cardContainer.querySelector('#id') as HTMLInputElement;
        const adminIdInput = cardContainer.querySelector('#adminId') as HTMLInputElement;

        if (nameInput && descriptionInput && projectIdInput && adminIdInput) {
            const updatedProject = await updateProject(
                nameInput.value,
                descriptionInput.value,
                projectIdInput.value,
                adminIdInput.value
            );

            if (updatedProject) {
                const cardBody = cardContainer.querySelector('.front');

                if (cardBody) {
                    cardBody.innerHTML = `
                        <h5 class="card-title">${updatedProject.name}</h5>
                        <p class="card-text">${updatedProject.description}</p>`;
                }

                // Flip the card back
                const cardInner = cardContainer.querySelector('.flip-card-inner');
                if (cardInner) {
                    cardInner.classList.toggle('flipped');
                }
            } else {
                console.error("One or more inputs not found");
            }
        } else {
            console.error("One or more inputs not found");
        }
    }, { once: true });
}
function handleEditButtonClick(button: Element) {
    const cardContainer = button.closest('.col-sm-12') as HTMLBodyElement;
    if (cardContainer) {
        const cardInner = cardContainer.querySelector('.flip-card-inner');
        if (cardInner) {
            cardInner.classList.toggle('flipped');

            setTimeout(() => {
                const updateProjectButton = cardContainer.querySelector("#updateProjectButton");
                if (updateProjectButton) {
                    addUpdateButtonListener(cardContainer, updateProjectButton);
                } else {
                    console.error("updateProjectButton not found");
                }
            }, 100);
        }
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const editButtons = document.querySelectorAll('.edit-btn');

    editButtons.forEach(button => {
        button.addEventListener('click', () => {
            handleEditButtonClick(button);
        });
    });
});