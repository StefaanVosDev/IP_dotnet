// Get one flow by id
export async function getFlow(id: string) {
    const response = await fetch(`/api/Flow/${id}`, {
        headers: {
            'Accept': 'application/json'
        }
    });
    if (!response.ok) {
        throw new Error(`Unable to get flow with id ${id}`)
    }
    return response.json();
}

// Update all flow values
export async function updateFlow(name: string, description: string, flowId: string) {
    const response = await fetch(`/api/Flows`, {
        method: 'PUT',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            ProjectId: flowId,
            NewName: name,
            NewDescription: description
        }),
    });

    if (!response.ok) {
        throw new Error(`Failed to update flow: ${response.statusText}`);
    }
}