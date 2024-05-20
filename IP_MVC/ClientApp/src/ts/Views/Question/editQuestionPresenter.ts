import * as client from "./restQuestionClient"

const questionId = (document.getElementById('questionId') as HTMLInputElement)?.value;
const min = (document.getElementById('min') as HTMLInputElement)?.value;
const max = (document.getElementById('max') as HTMLInputElement)?.value;
const titleText = document.getElementById('titleText')!;
const titleInput = document.getElementById('titleInput')!;
const optionTable = document.getElementById('optionTable');
const mediaUpload = document.getElementById('mediaUpload') as HTMLInputElement;

const updateRangeButton = document.getElementById('updateRangeValues')!;
const changeNameButton = document.getElementById('editButton')!;
const updateNameButton = document.getElementById('updateButton')!;
const uploadMediaButton = document.getElementById('uploadButton')!;

export async function showOptions() {
    
    const options = await client.getOptions(questionId);
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