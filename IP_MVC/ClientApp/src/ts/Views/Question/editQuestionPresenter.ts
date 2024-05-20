import * as client from "./restQuestionClient"

const titleText = document.getElementById('titleText')!;
const optionTable = document.getElementById('optionTable');

// Input elements
const questionId = document.getElementById('questionId') as HTMLInputElement;
const titleInput = document.getElementById('titleInput') as HTMLInputElement;
const mediaInput = document.getElementById('mediaUpload') as HTMLInputElement;
const newOption = document.getElementById('newOption') as HTMLInputElement;
const minInput = document.getElementById('min') as HTMLInputElement;
const maxInput = document.getElementById('max') as HTMLInputElement;

// Buttons
const changeNameButton = document.getElementById('editButton')!;
const updateNameButton = document.getElementById('updateButton')!;
const uploadMediaButton = document.getElementById('uploadButton')!;
const addOptionButton = document.getElementById('buttonToAdd')!;
const updateRangeButton = document.getElementById('updateRangeValues')!;


// Loads all options from question and shows them in table
export async function showOptions() {

    const options = await client.getOptions(questionId.value);
    options.forEach((option: string) => console.log(option));
    if (optionTable) {
        optionTable.innerHTML = '';

    }

    // optionTable.innerHTML = (await client.getOptions(questionId)).reduce(
    //     (acc: string, option: string) => `${acc}
    //     <tr>
    //     <td>${option}</td>
    //     </tr>`, "<table>"
    // ) + "</table>"
}

async function updateMedia(){
    
    //TODO: Error handling als geen file wordt geupload
    
    const file = mediaInput.files?.[0];
    
    if (file){
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


export default function addListeners() {
    changeNameButton.addEventListener('click', event => displayNameChange(true));
    updateNameButton.addEventListener('click', event => 
        client.updateQuestionTitle(questionId.value, titleInput.value)
    );
    uploadMediaButton.addEventListener('click', event => updateMedia());
    addOptionButton.addEventListener('click', event => 
        client.postOption(questionId.value, newOption.value).then(showOptions)
    );
    updateRangeButton.addEventListener('click', event => 
        client.updateQuestionRange(questionId.value, minInput.value, maxInput.value)
    );
}