// Get one project by id
export async function getProject(id: string) {
    const response = await fetch(`/api/Projects/${id}`, {
        headers: {
            'Accept': 'application/json'
        }
    });
    if (!response.ok) {
        throw new Error(`Unable to get project with id ${id}`)
    }
    return response.json();
}

// Update all project values
export async function updateProject(name: string, description: string, projectId: string, adminId: string) {
    const response = await fetch(`/api/Projects`, {
        method: 'PUT',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            ProjectId: projectId,
            AdminId: adminId,
            NewName: name,
            NewDescription: description
        }),
    });
   
    if (!response.ok) {
        throw new Error(`Failed to update project: ${response.statusText}`);
    }
}