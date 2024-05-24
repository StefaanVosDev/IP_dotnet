// Get one flow by id
export async function getFlow(projectId: string) {
    const response = await fetch(`/api/Flows/${projectId}`, {
        headers: {
            'Accept': 'application/json'
        }
    });
    if (!response.ok) {
        throw new Error(`Unable to get flow with id ${projectId}`)
    }
    return response.json();
}

// Update all flow values
export async function updateFlow(name: string, description: string, flowId:string) {
    const response = await fetch(`/api/Flows`, {
        method: 'PUT',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: flowId,
            NewName: name,
            NewDescription: description
        }),
    });

    if (!response.ok) {
        throw new Error(`Failed to update flow: ${response.statusText}`);
    }
}