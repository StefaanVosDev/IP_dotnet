import {Flows} from "../../models/Flows.interfaces";

// Get one flow by id
export async function getFlow(flowId: string) {
    const response = await fetch(`/api/Flows/${flowId}`, {
        headers: {
            'Accept': 'application/json'
        }
    });
    if (!response.ok) {
        throw new Error(`Unable to get flow with id ${flowId}`)
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

// Create a new flow
export async function createFlow(flow: Flows) {
    const response = await fetch(`/api/Flows`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(flow),
    });

    if (!response.ok) {
        throw new Error(`Failed to create flow: ${response.statusText}`);
    }
}

