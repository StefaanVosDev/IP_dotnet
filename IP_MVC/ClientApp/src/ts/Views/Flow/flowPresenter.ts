import * as client from "./restFlowClient"
import {createFlow} from "./restFlowClient";
import {Flows} from "../../models/Flows.interfaces";

const createButton = document.querySelectorAll('#createButton');

function setupEditEventListener(){
    const editButtons = document.querySelectorAll('.edit-flow-btn');
    const backButton = document.querySelectorAll('.btn-back');


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
}

createButton.forEach(button => {
    button.addEventListener('click', async () => {
        addFlow(button);
    })
});

async function showUpdatedFlow(id: string, projectCard: HTMLElement) {
    console.log('showFlow', id, projectCard);
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

    await showUpdatedFlow(flowIdInput.value, flowCard);

    const cardInner = flowCard.querySelector('.flip-card-inner');
    if (cardInner) {
        cardInner.classList.toggle('flipped');
    }
}

async function addFlow(addButton: Element) {
    const nameInput = document.getElementById('NewNameInput') as HTMLInputElement;
    const descriptionInput = document.getElementById('NewDescriptionInput') as HTMLInputElement;
    const projectIdInput = document.getElementById('projectIdInput') as HTMLInputElement;
    const parentFlowIdInput = document.getElementById('parentFlowIdInput') as HTMLInputElement;

    const flow: Flows = {
        NewName: nameInput.value,
        NewDescription: descriptionInput.value,
        NewProjectId: parseInt(projectIdInput.value),
        NewParentFlowId: parentFlowIdInput.value ? parseInt(parentFlowIdInput.value) : null,
        IsParentFlow: !parentFlowIdInput.value
    };

    try {
        await createFlow(flow);
        console.log('Flow created:', flow);
    } catch (error) {
        console.error('Error creating flow:', error);
        alert('There was an issue creating the flow. Please try again.');
    }

    appendFlowToPage(flow);
   setupEditEventListener();
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

function appendFlowToPage(flow: Flows) {
    const flowDataElement = document.getElementById('row');
    if (!flowDataElement) return;

    const isAdminRole = document.getElementById('userRole') as HTMLInputElement;
    const isActiveProject = document.getElementById('activeProject') as HTMLInputElement;
    let deleteButtonHtml = '';
    let editButtonHtml = '';

    if (isAdminRole && isAdminRole.value === "True" && isActiveProject && isActiveProject.value === "False") {
        deleteButtonHtml = `<a class="btn bi bi-trash" href="/Flow/Delete?flowId=${flow.NewProjectId}" onclick="return confirm('Are you sure you want to delete this flow?')"></a>`;
        editButtonHtml = `<button data-edit-flow data-flow-id="${flow.NewProjectId}" class="btn py-0 edit-flow-btn">Edit</button>`;
    }

    const flowActionsHtml = flow.IsParentFlow ? `
        <a href="/Flow/SubFlow?parentFlowId=${flow.NewParentFlowId}&active=${isActiveProject}" class="btn arrow-submit-right"></a>
    ` : `
        <a href="/Flow/PlayFlow?parentFlowId=${flow.NewParentFlowId}&FlowType=LINEAR" class="btn btn-primary">Start Flow</a>
        <a href="/Flow/PlayFlow?parentFlowId=${flow.NewParentFlowId}&FlowType=CIRCULAR" class="btn btn-primary">Start Circular Flow</a>
    `;

    const subFlowEditButtonHtml = `
        <a class="btn py-0" href="/Flow/Edit?parentFlowId=${flow.NewParentFlowId}">Edit SubFlows</a>
    `;

    flowDataElement.innerHTML += `
        <div class="col col-sm-12 col-md-6 col-lg-4 col-xl-4 pb-3">
            <div class="flip-card bg-transparent">
                <div class="flip-card-inner">
                    <div class="flip-card-front w-100">
                        <div class="small-card card border-1 border-black position-absolute card-clickable w-100" data-parent-flow-id="${flow.NewParentFlowId}">
                            <div class="front card-body overflow-y-scroll overflow-x-hidden">
                                <h5 class="card-title">${flow.NewName}</h5>
                                <p class="card-text">${flow.NewDescription}</p>
                            </div>
                            <div class="d-flex justify-content-between align-items-center position-sticky button-container overflow-x-scroll overflow-y-hidden px-3">
                                <div>${deleteButtonHtml}</div>
                                <div>${editButtonHtml}</div>
                                <div>${flowActionsHtml}</div>
                            </div>
                        </div>
                    </div>
                    <div class="flip-card-back w-100 position-relative">
                        <div class="small-card card border-1 border-black card-clickable" data-parent-flow-id="${flow.NewParentFlowId}">
                            <div class="card-body overflow-y-scroll overflow-x-hidden">
                                <input type="hidden" id="flowId" value="${flow.NewProjectId}"/>
                                <div class="form-group">
                                    <label for="nameInput">Name</label>
                                    <input type="text" class="form-control" id="nameInput" value="${flow.NewName}" required>
                                </div>
                                <div class="form-group">
                                    <label for="descriptionInput">Description</label>
                                    <input type="text" class="form-control" id="descriptionInput" value="${flow.NewDescription}" required>
                                </div>
                            </div>
                            <div class="d-flex justify-content-between align-items-center position-sticky button-container overflow-x-scroll overflow-y-hidden py-1">
                                <div><button class="btn py-0 btn-back">Back</button></div>
                                <div><button type="submit" class="btn py-0" id="updateFlowButton">Update</button></div>
                                <div>${subFlowEditButtonHtml}</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;
}

setupEditEventListener();