import * as client from "./restQuestionClient"
import {Flow} from "../../models/Flows.interfaces";

const titleText = document.getElementById('titleText')!;

const questionId = document.getElementById('questionId') as HTMLInputElement;
const questionType = document.getElementById('questionType') as HTMLInputElement;
const titleInput = document.getElementById('titleInput') as HTMLInputElement;
const mediaInput = document.getElementById('mediaUpload') as HTMLInputElement;

const changeNameButton = document.getElementById('editButton')!;
const updateNameButton = document.getElementById('updateButton')!;
const uploadMediaButton = document.getElementById('uploadButton')!;


if (questionType.value == "MultipleChoice" || questionType.value == "SingleChoice") {
    const newOption = document.getElementById('newOption') as HTMLInputElement;
    const addOptionButton = document.getElementById('buttonToAdd')!;
    const optionTable = document.getElementById('optionTable')!;

    // Loads all options from question and shows them in table
    async function showOptions() {
        try {
            optionTable.innerHTML = (await client.getOptions(questionId.value)).reduce(
                (acc: string, option: string) => `${acc}
                <tr>
                <td>${option}</td>
                <td><button delete-option option-id="${option}" type="button" class="btn btn-danger">Delete option</button></td>
                </tr>`, "<table>"
            ) + "</table>"
            console.clear();
        } catch (e) {
            optionTable.innerHTML = '<p>This question does not have any options yet</p>'
            console.error("Error showing options in table: ", e);
        }
    }

    async function removeOption(element: HTMLElement) {
        if (element.hasAttribute('delete-option')) {
            let option = element.getAttribute("option-id");
            if (option != null) {
                try {
                    await client.deleteOption(questionId.value, option);
                } catch (e) {
                    console.log(`ERROR DELETING OPTION ${option}`, e);
                }
            }
            await showOptions();
        }
    }

    async function addOption(option: string) {
        try {
            await client.postOption(questionId.value, option)
        } catch (e) {
            console.error("Error adding option: ", e);
        }
        await showOptions();
        newOption.value = '';
    }

    showOptions();

    document.addEventListener('click', event => removeOption(event.target as HTMLElement));
    addOptionButton.addEventListener('click', event => {
        event.preventDefault();
        addOption(newOption.value);
    });


} else if (questionType.value == "Range") {
    const minInput = document.getElementById('min') as HTMLInputElement;
    const maxInput = document.getElementById('max') as HTMLInputElement;
    const updateRangeButton = document.getElementById('updateRangeValues')!;

    updateRangeButton.addEventListener('click', event => {
            event.preventDefault();
            client.updateQuestionRange(questionId.value, minInput.value, maxInput.value);
        }
    );
}

async function updateMedia() {

    //TODO: Error handling als geen file wordt geupload

    const file = mediaInput.files?.[0];

    if (file) {
        const formData = new FormData();
        formData.append('file', file);
        formData.append('questionId', questionId.value);

        await client.postMedia(formData);
    }
}

// display the iput element to change question name
function displayNameChange(display: boolean) {
    if (display) {
        titleText.style.display = 'none';
        changeNameButton.style.display = 'none';
        titleInput.style.display = 'block';
        updateNameButton.style.display = 'block';
    } else {
        titleText.style.display = 'block';
        changeNameButton.style.display = 'block';
        titleInput.style.display = 'none';
        updateNameButton.style.display = 'none';
    }
}


changeNameButton.addEventListener('click', event => displayNameChange(true));
updateNameButton.addEventListener('click', event =>
    client.updateQuestionTitle(questionId.value, titleInput.value)
);


uploadMediaButton.addEventListener('click', event => updateMedia());