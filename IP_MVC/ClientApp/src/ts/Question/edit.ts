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
        fetch(`https://localhost:7292/Question/UpdateTitle?id=${questionId}&text=${newTitle}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(r => {
            if (!r.ok) {
                throw new Error('Network response was not ok');
            }}
        );
    }
});

// Loading all earlier options for single- or multiple choice question
function loadAllOptions() {
    const lijst = document.getElementById('listAnswers');
    if (lijst) {
        const questionId = (document.getElementById('questionId') as HTMLInputElement)?.value;
        
        fetch(`https://localhost:7292/Question/GetOptions?id=${questionId}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                // first make the list empty
                while (lijst.firstChild) {
                    lijst.removeChild(lijst.firstChild);
                }
                
                return response.json();
            })
            .then(data => {
                // Check if data is not empty before processing
                if (data && data.length > 0) {
                    data.forEach((option:string) => {
                        addOptionToUI(option);
                    });
                } else {
                    console.log('No options found');
                }
            })
            .catch(error => console.error('Error fetching options:', error));
    }
}

loadAllOptions()

document.getElementById('buttonToAdd')?.addEventListener('click', function(event) {
    event.preventDefault();

    const questionId = (document.getElementById('questionId') as HTMLInputElement)?.value;
    const newOption = (document.getElementById('newOption') as HTMLInputElement)?.value;

    // Optimistically add the new option to the UI
    addOptionToUI(newOption);

    fetch(`https://localhost:7292/api/Questions/UpdateMultipleChoiceQuestion?id=${questionId}&option=${newOption}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(
            //make the input field empty again
            () => (document.getElementById('newOption') as HTMLInputElement).value = ''
        )
        .catch(error => {
            console.error('Error adding option:', error);
            // If the fetch request fails, revert the UI back to its original state
            removeOptionFromUI(newOption);
        });
});

function addOptionToUI(option: string) {
    const lijst = document.getElementById('listAnswers');
    if (lijst) {
        const newOption = document.createElement('div');
        newOption.innerText = option;
        newOption.setAttribute('data-multiplechoice-option', '');

        // const optionText = document.createElement('span');
        // optionText.innerText = option;

        const deleteButton = document.createElement('button');
        deleteButton.innerText = 'Delete option';
        deleteButton.className = 'btn btn-primary delete-option';

        const questionId = (document.getElementById('questionId') as HTMLInputElement)?.value;
        deleteButton.addEventListener('click', function (event: Event) {
            event.preventDefault();
            fetch(`https://localhost:7292/api/Questions/DeleteOption?id=${questionId}&option=${option}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    // If the deletion was successful, remove the option from the UI
                    if (newOption.parentNode) {
                        newOption.parentNode.removeChild(newOption);
                    }
                })
                .catch(error => {
                    console.error('Error deleting option:', error);
                    // If the fetch request fails, revert the UI back to its original state
                    addOptionToUI(option);
                });
        });

        // newOption.appendChild(optionText);
        newOption.appendChild(deleteButton);
        lijst.appendChild(newOption);
    }
}

function removeOptionFromUI(option: string) {
    const lijst = document.getElementById('listAnswers');
    if (lijst) {
        const options = lijst.querySelectorAll('[data-multiplechoice-option]') as NodeListOf<HTMLDivElement>;
        options.forEach((optionElement) => {
            if (optionElement.innerText === option) {
                lijst.removeChild(optionElement);
            }
        });
    }
}


//Adding media
document.getElementById('uploadButton')?.addEventListener('click', function(event) {
    event.preventDefault();

    const mediaUpload = document.getElementById('mediaUpload') as HTMLInputElement;
    const file = mediaUpload.files?.[0];
    const questionId = (document.getElementById('questionId') as HTMLInputElement)?.value;

    if (file) {
        const formData = new FormData();
        formData.append('file', file);
        formData.append('questionId', questionId)

        fetch('https://localhost:7292/api/Questions/UploadMedia', {
            method: 'POST',
            body: formData
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                console.log('File uploaded successfully');
            })
            .catch(error => console.error('Error uploading file:', error));
    }
});