document.addEventListener("DOMContentLoaded", init);

function init() {
    const updateProjectButton = document.getElementById("updateProjectButton") as HTMLButtonElement;
    const nameInput = document.getElementById('nameInput') as HTMLInputElement;
    const descriptionInput = document.getElementById('descriptionInput') as HTMLInputElement;
    const projectIdInput = document.getElementById('id') as HTMLInputElement;
    const adminIdInput = document.getElementById('adminId') as HTMLInputElement;

    const urlParams = getUrlParams();
    setInputValues(urlParams, projectIdInput, adminIdInput, nameInput, descriptionInput);

    if (updateProjectButton) {
        updateProjectButton.addEventListener("click", () => updateProject(projectIdInput, adminIdInput, nameInput, descriptionInput));
    } else {
        console.error('Update project button not found');
    }
}

function getUrlParams() {
    const urlParams = new URLSearchParams(window.location.search);
    return {
        projectId: urlParams.get('Id'),
        adminId: urlParams.get('AdminId'),
        projectName: urlParams.get('Name'),
        projectDescription: urlParams.get('Description')
    };
}

function setInputValues(params: any, projectIdInput: HTMLInputElement, adminIdInput: HTMLInputElement, nameInput: HTMLInputElement, descriptionInput: HTMLInputElement) {
    if (params.projectId && params.adminId && params.projectName && params.projectDescription) {
        projectIdInput.value = params.projectId;
        adminIdInput.value = params.adminId;
        nameInput.value = params.projectName;
        descriptionInput.value = params.projectDescription;
    } else {
        console.error('Required URL parameters are missing');
    }
}

async function updateProject(projectIdInput: HTMLInputElement, adminIdInput: HTMLInputElement, nameInput: HTMLInputElement, descriptionInput: HTMLInputElement) {
    try {
        const response = await fetch(`/api/Projects`, {
            method: 'PUT',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                ProjectId: projectIdInput.value,
                AdminId: adminIdInput.value,
                NewName: nameInput.value,
                NewDescription: descriptionInput.value
            }),
        });

        if (response.ok) {
            const project = await response.json();
            console.log('Project updated successfully:', project);
        } else {
            const errorText = await response.text();
            throw new Error(`Failed to update project: ${errorText}`);
        }
    } catch (error) {
        console.error('Error updating project:', error);
        alert('There was an error updating the project. Please try again.');
    }
}
