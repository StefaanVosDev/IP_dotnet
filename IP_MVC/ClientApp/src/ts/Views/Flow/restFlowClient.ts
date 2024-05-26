import {Flows} from "../../models/Flows.interfaces";
import {appendFlowToPage, appendSubFlowToPage, setupEditEventListener} from "./flowPresenter";

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

// Get one flow by id
export async function getFlowId(): Promise<string> {
    const urlSearchParams = new URLSearchParams(window.location.search)

    let clothingItemid = urlSearchParams.get("projectId")

    if (!clothingItemid) {
        const path = window.location.pathname
        clothingItemid = path.substring(path.lastIndexOf("/") + 1)
    }

    return clothingItemid
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
            Id: flowId,
            NewName: name,
            NewDescription: description
        }),
    }) 
}

// Create a new flow
export async function createFlow(flow: Flows) {
    const response = await fetch(`/api/Flows`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(flow),
    }).then(response => response.json())
        .then(data => {
            if (flow.NewParentFlowId == null){
                appendFlowToPage(flow, data.id)
            } else {
                appendSubFlowToPage(flow, data.Id);
            }
            setupEditEventListener();
        }) .catch(reason => alert("Error updating flow:" + reason));
}

export async function getSubFlows(flowId: string) {
    const response = await fetch(`/api/Flows/${flowId}/SubFlows`, {
        headers: {
            'Accept': 'application/json'
        }
    });
    if (!response.ok) {
        throw new Error(`Unable to get subflows for flow with id ${flowId}`)
    }
    return response.json();
}