import * as client from "../question/restQuestionClient";
import Sortable from "sortablejs";

async function reorderQuestions() {
    const tbody = document.querySelector('.table tbody');
    if (tbody) {
        new Sortable(<HTMLElement>tbody, {
            onUpdate: function (evt) {
                const questionId = evt.item.getAttribute('data-question-id') as string;
                const position = 1 + <number>evt.newIndex;
                client.reOrderQuestions(questionId, position);
            },
        });
    }
}

reorderQuestions();