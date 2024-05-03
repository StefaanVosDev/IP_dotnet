const buttons = Array.from(document.querySelectorAll('button')).filter(input => {
    const name = input.getAttribute('name');
    return name && /^redirectedQuestionId\d+$/.test(name);
});

console.log("Dit zijn de buttons")
console.log(buttons)
buttons.forEach(button => {
    button.addEventListener('click', function(event) {
        const name = button.getAttribute('name') as string;
        const lasteCharOfName = name[name.length - 1];
        const nameId = parseInt(lasteCharOfName);
        
        const redirectedQuestionIdInput = document.getElementById("redirectedQuestionId") as HTMLInputElement;
        if (redirectedQuestionIdInput) {
            redirectedQuestionIdInput.value = nameId.toString();
        }
    });
});