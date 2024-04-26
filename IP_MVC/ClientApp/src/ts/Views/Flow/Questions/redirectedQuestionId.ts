function setRedirectedQuestionId(value: string): void {
    const redirectedQuestionIdInput = document.getElementsByName("redirectedQuestionId")[0] as HTMLInputElement;
    if (redirectedQuestionIdInput) {
        redirectedQuestionIdInput.value = value;
    }
}