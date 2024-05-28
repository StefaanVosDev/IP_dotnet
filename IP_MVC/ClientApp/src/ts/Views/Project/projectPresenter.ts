import * as client from "./restProjectClient"
import {Project} from "../../models/Project.interface";
import {setupEditEventListener} from "../Question/createQuestionPresenter";

const editButtons = document.querySelectorAll('.edit-btn');
const backButton = document.querySelectorAll('.btn-back');
const createProjectButton = document.getElementById('createProjectButton') as HTMLButtonElement;

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

createProjectButton.addEventListener('click', async (event) => {
    event.preventDefault();
    const nameInput = document.querySelector("textarea[name='Name']") as HTMLTextAreaElement;
    const descriptionInput = document.querySelector("textarea[name='Description']") as HTMLTextAreaElement;
    
    const name = nameInput.value;
    const description = descriptionInput.value;
    try {
        const project = await client.createProject(name, description);
        showProject(project);
        const popup = document.getElementById('popupOverlay');
        if (popup) {
            popup.style.display = 'none';
        }
    } catch (error) {
        console.error('Error creating project:', error);
    }
});

export function showProject(project: Project) {

    const projectsContent = document.getElementById('projects') as HTMLTableElement;

    let deleteButtonHtml = `<a class="btn btn-blue py-0 bi bi-trash" href="/Project/Delete?parentFlowId=${project.Id}" onclick="return confirm('Are you sure you want to delete this project?')"></a>`;
    let analyticButtonHtml = `<a class="btn btn-blue py-0 bi bi-graph-up" href="/Analytic/Analytic?projectId=${project.Id}"></a>`;
    let startSessionButtonHtml = `<a class="btn btn-blue py-0" href="/Flow/ActivateProject?projectId=${project.Id}&active=true">Start</a>`;
    let editFlowsButtonHtml = `<a class="btn btn-blue py-0" href="/Flow/Flow?projectId=${project.Id}">Flows</a>`;
    let manageFacilitatorsButtonHtml = `<a class="btn btn-blue py-0" id="popupButton" href="/Project/ManageFacilitators?projectId=${project.Id}">Facilitators</a>`;

    

    let randomIndex = Math.floor(Math.random() * (5 - 1 + 1)) + 1;
    let imageUrl = `https://storage.googleapis.com/phygital-public/Flows/flow_page_hands_${randomIndex}.png`;

    projectsContent.innerHTML += ` 
             <div class="col-sm-12 col-md-6 col-lg-4 col-xl-4 py-0">
                <div class="flip-card">
                    <div class="flip-card-inner">
                        <div class="flip-card-front">
                            <img src="${imageUrl}" class="card-img-top w-100 h-100 z-1 position-relative" alt="Afbeelding_van_flow">
                            <div class="card border-1 border-black position-absolute card-clickable" data-parent-flow-id="${project.Id}">
                                <div class="front card-body overflow-y-scroll overflow-x-hidden">
                                    <h5 class="card-title">${project.NewName}</h5>
                                    <p class="card-text">${project.NewDescription}</p>
                                </div>
                                <div class="d-flex justify-content-between px-sm-2 px-md-3 px-lg-4 px-xl-4">
                                    <div class="btn-group">
                                        <button class="btn bi bi-menu-button-wide btn-blue dropdown-toggle py-0" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                        </button>
                                        <ul class="dropdown-menu dropdown-menu-left background-color-light-blue min-vw-auto">
                                            <li class="px-1 pb-1">
                                                ${deleteButtonHtml}
                                            </li>
                                            <li class="px-1">
                                                ${analyticButtonHtml}
                                            </li>
                                        </ul>
                                    </div>
                                    <div>
                                        <button data-project-id="${project.Id}" class="btn py-0 btn-blue edit-btn">Edit</button>
                                    </div>
                                    <div>
                                        ${startSessionButtonHtml}
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="flip-card-back position-relative">
                            <img src="${imageUrl}" class="card-img-top w-100 h-100 z-1 position-relative" alt="Afbeelding_van_flow">
                            <div class="card border-1 border-black h-50 position-absolute card-clickable" data-parent-flow-id="${project.Id}">
                                <div class="card-body overflow-y-scroll overflow-x-hidden">
                                    <input type="hidden" id="id" value="${project.Id}"/>
                                    <input type="hidden" id="adminId" value="${project.AdminId}"/>

                                    <div class="form-group">
                                        <label for="nameInput">Name</label>
                                        <input type="text" class="form-control" id="nameInput" value="${project.NewName}" required>
                                    </div>
                                    <div class="form-group">
                                        <label for="descriptionInput">Description</label>
                                        <input type="text" class="form-control" id="descriptionInput" value="${project.NewDescription}" required>
                                    </div>
                                </div>

                                <div class="d-flex justify-content-between button-container px-sm-2 px-md-3 px-lg-4 px-xl-4">
                                    <div>
                                        <button class="btn btn-blue bi bi-box-arrow-left btn-back py-0"></button>
                                    </div>
                                    <div>
                                        <button type="submit" class="btn btn-blue py-0" id="updateProjectButton">Update</button>
                                    </div>
                                    <div class="btn-group p-0">
                                        <button class="btn bi bi-menu-button-wide btn-blue dropdown-toggle p-0" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                        </button>
                                        <ul class="dropdown-menu dropdown-menu-right background-color-light-blue min-vw-auto">
                                            <li class="px-1 pb-1">
                                                ${editFlowsButtonHtml}
                                            </li>
                                            <li class="px-1">
                                               ${manageFacilitatorsButtonHtml}
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

    `;

    setupEditEventListener();
}
