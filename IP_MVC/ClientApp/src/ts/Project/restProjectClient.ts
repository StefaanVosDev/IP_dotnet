// Update aal project values
export async function updateProject(name: string, description: string, projectId: string, adminId: string) {
    try {
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
        return await response.json();
    } catch (error) {
        console.error('Error updating project:', error);
        alert('There was an issue updating the project. Please try again.');
    }
}