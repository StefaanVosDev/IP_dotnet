const deleteButtons = document.querySelectorAll('[data-delete-post]');
deleteButtons.forEach(deleteButton => {
    deleteButton.addEventListener('click', function() {
        const articleRow = deleteButton.closest('tr');
        if (articleRow) {
            articleRow.remove();
        }
    });
});