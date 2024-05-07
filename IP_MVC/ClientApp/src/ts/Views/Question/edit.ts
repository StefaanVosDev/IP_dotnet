document.querySelectorAll<HTMLButtonElement>('.delete-option').forEach(button => {
    button.addEventListener('click', function(this: HTMLButtonElement) {
        // Remove the option div
        if (this.parentNode) {
            this.parentNode.removeChild(this);
        }
    });
});

document.getElementById('buttonToAdd')?.addEventListener('click', function(event) {
    event.preventDefault();

    const questionId = (document.getElementById('questionId') as HTMLInputElement)?.value;
    const newOption = (document.getElementById('newOption') as HTMLInputElement)?.value;

    fetch(`https://localhost:7292/api/Questions/UpdateMultipleChoiceQuestion?id=${questionId}&option=${newOption}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            // Add the new option to the DOM
            const listAnswers = document.getElementById('listAnswers');
            const optionDiv = document.createElement('div');
            optionDiv.innerText = newOption;
            optionDiv.setAttribute('data-multiplechoice-option', '');
            listAnswers?.appendChild(optionDiv);
        })
        .then(loadAllOptions)
        .catch(error => console.error('Error adding option:', error));
});

document.getElementById('updateRangeValues')?.addEventListener('click', function(event) {
    event.preventDefault();

    const questionId = (document.getElementById('questionId') as HTMLInputElement)?.value;
    const min = (document.getElementById('min') as HTMLInputElement)?.value;
    const max = (document.getElementById('max') as HTMLInputElement)?.value;

    fetch(`https://localhost:7292/api/Questions/UpdateRangeQuestion?id=${questionId}&min=${min}&max=${max}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
        })
        .catch(error => console.error('Error updating range values:', error));
});


document.getElementById('editButton')?.addEventListener('click', function(event) {
    event?.preventDefault();

    const titleText = document.getElementById('titleText');
    const editButton = document.getElementById('editButton');
    const titleInput = document.getElementById('titleInput');
    const updateButton = document.getElementById('updateButton');

    if (titleText && editButton && titleInput && updateButton) {
        titleText.style.display = 'none';
        editButton.style.display = 'none';
        titleInput.style.display = 'block';
        updateButton.style.display = 'block';
    }
});

// Update new the question title
document.getElementById('updateButton')?.addEventListener('click', function(event) {
    event?.preventDefault();

    const newTitle = (document.getElementById('titleInput') as HTMLInputElement).value;
    const titleText = document.getElementById('titleText') as HTMLDivElement;
    const editButton = document.getElementById('editButton');
    const titleInput = document.getElementById('titleInput');
    const updateButton = document.getElementById('updateButton');

    if (titleText && editButton && titleInput && updateButton) {
        titleText.innerText = newTitle;
        titleText.style.display = 'block';
        editButton.style.display = 'block';
        titleInput.style.display = 'none';
        updateButton.style.display = 'none';

        const questionId = (document.getElementById('questionId') as HTMLInputElement)?.value;

        // Send new title to server
        fetch(`https://localhost:7292/api/Questions/UpdateTitle?id=${questionId}&text=${newTitle}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }
});

// Loading all earlier options for single- or multiple choice question
function loadAllOptions() {
    const lijst = document.getElementById('listAnswers');
    if (lijst) {
        lijst.innerHTML = 'de lijst kan worden geladen';
        
        const questionId = (document.getElementById('questionId') as HTMLInputElement)?.value;

        fetch(`https://localhost:7292/api/Questions/GetOptions?id=${questionId}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                // Check if data is not empty before processing
                if (data && data.length > 0) {
                    data.forEach((option:string) => {
                        const newOption = document.createElement('div');
                        newOption.innerText = option;
                        newOption.setAttribute('data-multiplechoice-option', '');
                        lijst.appendChild(newOption);

                        //also add an option to delete this option from the list
                        const deleteButton = document.createElement('button');
                        deleteButton.innerText = 'Delete option';
                        deleteButton.className = 'btn btn-primary delete-option';
                        deleteButton.addEventListener('click', function(this: HTMLButtonElement) {
                            // Remove the option div
                            if (this.parentNode) {
                                this.parentNode.removeChild(this);
                            }
                            //also delete the option from the database
                            const questionId = (document.getElementById('questionId') as HTMLInputElement)?.value;

                            fetch(`https://localhost:7292/api/Questions/DeleteOption?id=${questionId}&option=${option}`, {
                                method: 'POST',
                                headers: {
                                    'Content-Type': 'application/json'
                                }
                            });
                        });                        
                    });
                } else {
                    console.log('No options found');
                }
            })
            .catch(error => console.error('Error fetching options:', error));
    }
}

// Loading the min and max value for a range question
function loadMinMax() {
    fetch(`https://localhost:7292/api/Questions/GetMinMax?id=${(document.getElementById('questionId') as HTMLInputElement)?.value}`)
        .then(response => response.json())
        .then(data => {
            (document.getElementById('min') as HTMLInputElement).value = data.min;
            (document.getElementById('max') as HTMLInputElement).value = data.max;
        });
}

// Switch to use the correct funtion
function loadCorrectData() {
    switch ((document.getElementById('questionType') as HTMLInputElement)?.value) {
        case 'Range':
            loadMinMax();
            break;
        case 'MultipleChoice':
            loadAllOptions();
            break;
        case 'Open':
            break;
        case 'SingleChoice':
            loadAllOptions();
            break;
        default:
            break;
    }
}

loadCorrectData();
