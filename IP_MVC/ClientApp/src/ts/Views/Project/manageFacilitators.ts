const searchBox = document.getElementById('searchBox') as HTMLInputElement;
const searchResult = document.getElementById('searchResult') as HTMLDivElement;
const currentFacilitators = document.getElementById('currentFacilitators') as HTMLDivElement;
const projectIdElement = document.getElementById('projectId') as HTMLInputElement;
const projectId = projectIdElement.value;

searchBox.addEventListener('keyup', function () {
    const searchTerm = searchBox.value;
    if (searchTerm.length > 2) {
        loadFacilitators(searchTerm);
    } else if (searchTerm.length === 0) {
        searchResult.innerHTML = '';
    }
});

const loadFacilitators = (searchTerm: string) => {
    fetch(`/api/Projects/ManageFacilitators?searchTerm=${searchTerm}`)
        .then(response => response.json())
        .then((data: string[]) => {
            searchResult.innerHTML = '';
            data.forEach((user: string) => {
                const userElement = document.createElement('div');
                userElement.textContent = user;
                userElement.addEventListener('click', function () {
                    fetch(`/api/Projects/AddUser`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({userName: user, projectId: projectId})
                    })
                        .then(() => {
                            const li = document.createElement('li');
                            li.textContent = user;
                            currentFacilitators.appendChild(li);
                            loadFacilitators(searchBox.value);
                        });
                });
                searchResult.appendChild(userElement);
            });
        });
};

