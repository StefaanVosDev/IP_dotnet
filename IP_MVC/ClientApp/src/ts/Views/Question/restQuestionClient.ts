// Update question title 
export async function updateQuestionTitle(questionId: string, title: string) {
    try {
        const response = await fetch(`https://localhost:7292/Question/UpdateTitle?id=${questionId}&text=${title}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
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

// Get all options from a question
export async function getOptions(questionId: string) {
    try {
        const response = await fetch("https://localhost:7292/Question/GetOptions?id=" + questionId);
        if (!response.ok) {
            throw Error(`Unable to get options: ${response.status} ${response.statusText}`);
        }
        return response.json();
    } catch (e) {
        console.error(e);
        throw e;
    }
}

// Delete an option from a question
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

// Add Media to a question
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