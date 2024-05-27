export interface Flow {
    NewName: string;
    NewDescription: string;
    NewProjectId: number;
    NewParentFlowId: number | null;
    IsParentFlow: boolean; 
}