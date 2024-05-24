window.addEventListener('load', function() {
    const rangeInput = document.querySelector('input[type="range"]') as HTMLInputElement;
    const selectedValueInput = document.getElementById('selectedValue') as HTMLInputElement;
    const options = Array.from(document.querySelectorAll('.form-check-input')) as HTMLInputElement[];
    let selectedIndex = 0;
    let aKeyPressCount = 0;
    let lastAKeyPressTime = 0;
    const debounceTime = 500;
    let intervalId: NodeJS.Timeout;
    

    document.addEventListener('keydown', function(event: KeyboardEvent) {
        switch (event.key) {
            case 'a':
            case 'ArrowLeft':
                (document.getElementById('prevQuestionButton') as HTMLButtonElement).click();
                break;
            case 'd':
            case 'ArrowRight':
                aKeyPressCount++;
                if (aKeyPressCount === 1) {
                    lastAKeyPressTime = new Date().getTime();
                    intervalId = setInterval(() => {
                        const currentTime = new Date().getTime();
                        if (currentTime - lastAKeyPressTime > debounceTime) {
                            options[selectedIndex].checked = !options[selectedIndex].checked;
                            aKeyPressCount = 0;
                            clearInterval(intervalId);
                        }
                    }, debounceTime);
                } else if (aKeyPressCount === 2 && new Date().getTime() - lastAKeyPressTime <= debounceTime) {
                    if (selectedIndex >= 0) {
                        (document.getElementById('nextQuestionButton') as HTMLButtonElement).click();
                    }
                    aKeyPressCount = 0;
                    clearInterval(intervalId);
                }
                break;
            case 'w':
            case 'ArrowUp':
                if (selectedIndex > 0) {
                    options[selectedIndex].parentElement?.classList.remove('selected-option');
                    selectedIndex--;
                    options[selectedIndex].parentElement?.classList.add('selected-option');
                }
                if (rangeInput.valueAsNumber < parseInt(rangeInput.max)) {
                    rangeInput.valueAsNumber++;
                    selectedValueInput.value = rangeInput.value;
                }
                break;
            case 's':
            case 'ArrowDown':
                if (selectedIndex >= 0 && selectedIndex < options.length - 1) {
                    options[selectedIndex].parentElement?.classList.remove('selected-option');
                    selectedIndex++;
                    options[selectedIndex].parentElement?.classList.add('selected-option');
                }
                if (rangeInput.valueAsNumber > parseInt(rangeInput.min)) {
                    rangeInput.valueAsNumber--;
                    selectedValueInput.value = rangeInput.value;
                }
                break;
            case 'q':
                if (selectedIndex >= 0) {
                    options[selectedIndex].checked = !options[selectedIndex].checked;
                }
                break;
        }
    });
});
