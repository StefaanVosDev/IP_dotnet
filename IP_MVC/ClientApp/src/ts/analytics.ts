import {Chart} from "chart.js";

const flowAnalyticsElement: HTMLCanvasElement | null = document.querySelector('#flowAnalytics');
let ctx: CanvasRenderingContext2D | null = null;
if (flowAnalyticsElement) {
    ctx = flowAnalyticsElement.getContext('2d');
}
let chart: Chart | undefined;

document.querySelector('#Flows')?.addEventListener('change', async (e) => {
    const flowId = (e.target as HTMLSelectElement).value;
    const response = await fetch(`/api/AnalyticsApi/GetFlowAnalytics/${flowId}`);
    const data = await response.json();

    const chartsContainer: HTMLElement | null = document.querySelector('#chartsContainer');
    if (chartsContainer) {
        chartsContainer.innerHTML = '';
    }
    
    data.forEach((item: any, index: number) => {
        const canvas = document.createElement('canvas');
        canvas.id = `flowAnalytics${index}`;
        chartsContainer?.appendChild(canvas);

        const ctx = canvas.getContext('2d');

        if (ctx) {
            new Chart(ctx, {
                type: item.type,
                data: item.data,
                options: item.options
            });
        }
    });
});