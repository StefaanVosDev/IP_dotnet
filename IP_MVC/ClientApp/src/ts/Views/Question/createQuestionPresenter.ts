import * as client from "./restQuestionClient";
import {Flow} from "../../models/Flows.interfaces";
import {updateSwiper} from "../Flow/createScroll";
import {Question} from "../../models/Questions.interfaces";

const createQuestionButton = document.querySelectorAll('#createButton')!;
const saveAndRedirectButton = document.querySelectorAll('#saveAndRedirectButton');

export function setupEditEventListener(){
    createQuestionButton.forEach(button => {
        button.addEventListener('click', async () => {
            addQuestion().then(() => {
                const textInput = document.getElementById('newQuestionText') as HTMLInputElement;
                const typeInput = document.getElementById('newQuestionType') as HTMLInputElement;
                textInput.value = '';
                typeInput.value = '';
            });
        })
    });

    saveAndRedirectButton.forEach(button => {
        button.addEventListener('click', async () => {
            const form = document.getElementById('flowEditForm') as HTMLFormElement;
            if (form) {
                form.submit();
            }
        })
    });
    deleteQuestion();
}

async function addQuestion() {
    const textInput = document.getElementById('newQuestionText') as HTMLInputElement;
    const typeInput = document.getElementById('newQuestionType') as HTMLSelectElement;
    const flowIdInput = document.getElementById('flowIdInput') as HTMLInputElement;
    try {
        await client.createQuestion(textInput.value, typeInput.value, flowIdInput.value);
    } catch (error) {
        console.error('Error creating flow:', error);
        alert('There was an issue creating the flow. Please try again.');
    }
}

export function showQuestion(question: Question) {

    const questionsContent = document.getElementById('questions') as HTMLTableElement;
    let deleteButtonHtml = '';
    let editButtonHtml = '';

    deleteButtonHtml = `<button class="btn btn-blue deleteQuestionButton bi bi-trash" data-question-id="${question.Id}"  href="/Question/Delete?questionId=${question.Id}"></button>`;
    editButtonHtml = ` <a class="btn btn-blue py-0" href="/Question/Edit?questionId=${question.Id}" class="btn btn-primary">Edit</a>`;

    questionsContent.innerHTML += `
        <tbody>
        <tr>
            <td class="question" data-question-id="@{question.Text}" data-position="@question.Position">${question.NewText}</td>
            <td class="align-content-center">
                <div class="row">
                    <div class="col-6 ">
                        <div>${editButtonHtml}</div>
                    </div>
                    <div class="col-6">
                        <div>${deleteButtonHtml}</div>
                    </div>
            </div>
        </td>
        </tr>
        </tbody>
    `;

    setupEditEventListener();
}

async function deleteQuestion() {
    const deleteButtonsForQuestion = document.getElementsByClassName('deleteQuestionButton') as HTMLCollectionOf<HTMLButtonElement>
    if (deleteButtonsForQuestion === null) return;

    for (let i = 0; i < deleteButtonsForQuestion.length; i++) {
        deleteButtonsForQuestion[i].addEventListener('click', async (event) => {
            event.preventDefault();

            if (confirm('Are you sure you want to delete this flow?')) {
                window.location.href = deleteButtonsForQuestion[i].getAttribute('href') as string;
            }

        });
    }
}

setupEditEventListener();