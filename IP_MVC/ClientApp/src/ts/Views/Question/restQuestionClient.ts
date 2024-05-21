// Get all options from question
export async function getOptions(questionId: string) {
    try {
        const response = await fetch(`https://localhost:7292/api/Questions/${questionId}/Options`);
        if (!response.ok) {
            throw Error(`Unable to get options: ${response.status} ${response.statusText}`);
        }
        return response.json();
    } catch (e) {
        console.error(e);
        throw e;
    }
}

// Update question title 
export async function updateQuestionTitle(questionId: string, title: string) {
    try {
        const response = await fetch(`https://localhost:7292/api/Questions/${questionId}/Title`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(title)
        });
        if (!response.ok) {
            throw new Error('Unable to update title of question');
        }
    } catch (e) {
        console.error(e);
        throw e;
    }
}

// Update range from question
export async function updateQuestionRange(questionId: string, min: string, max: string) {
    try {
        const response = await fetch(`https://localhost:7292/api/Questions/UpdateRangeQuestion?id=${questionId}&min=${min}&max=${max}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            throw new Error('Unable to update range of question');
        }
    } catch (e) {
        console.error(e);
        throw e;
    }
}

// Add option from to question
export async function postOption(questionId: string, newOption: string) {
    try {
        const response = await fetch(`https://localhost:7292/api/Questions/${questionId}/Option`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newOption)
        });
        if (!response.ok) {
            throw new Error('Unable to add option to question');
        }
    } catch (e) {
        console.error(e);
        throw e;
    }
} 

// Add Media to question
export async function postMedia(formData: FormData) {
    try {
        const response = await fetch('https://localhost:7292/api/Questions/UploadMedia', {
            method: 'POST',
            body: formData
        });
        if (!response.ok) {
            throw new Error('Unable to post media');
        }
    } catch (e) {
        console.error(e);
        throw e;
    }
}

// Delete an option from question
export async function deleteOption(questionId: string, option: string) {
    try {
        const response = await fetch(`https://localhost:7292/api/Questions/DeleteOption?id=${questionId}&option=${option}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            throw new Error('Unable to delete option from question');
        }
    } catch (e) {
        console.error(e);
        throw e;
    }
}
