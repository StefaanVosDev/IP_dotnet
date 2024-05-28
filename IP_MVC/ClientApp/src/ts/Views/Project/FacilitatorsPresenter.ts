import *  as FacilitatorClient from "./restFacilitatorClient";

const searchBox = document.getElementById('searchBox') as HTMLInputElement;
const searchResult = document.getElementById('searchResult') as HTMLDivElement;
const projectIdElement = document.getElementById('projectId') as HTMLInputElement;
const projectId = projectIdElement.value;

searchBox.addEventListener('keyup', function() {
    const searchTerm = searchBox.value;
    if (searchTerm.length > 2) {
        loadFacilitators(searchTerm);
    } else if (searchTerm.length === 0) {
        searchResult.innerHTML = '';
    }
});

function loadFacilitators(searchTerm: string) {
    FacilitatorClient.fetchFacilitators(searchTerm)
        .then((data: string[]) => {
            displaySearchResults(data);
        });
}

function displaySearchResults(data: string[]) {
    searchResult.innerHTML = '';
    data.forEach((user: string) => {
        const userElement = createFacilitatorElement(user);
        searchResult.appendChild(userElement);
    });
}

function createFacilitatorElement(user: string) {
    const userElement = document.createElement('div');
    userElement.textContent = user;
    userElement.addEventListener('click', function() {
        handleFacilitatorClick(user);
    });
    return userElement;
}

function handleFacilitatorClick(user: string) {
    FacilitatorClient.addUserToProject(user, projectId)
        .then(() => {
            displayFacilitator(user);
            loadFacilitators(searchBox.value);
        });
}

function displayFacilitator(user: string) {
    const userPrint = document.getElementById('printFacilitators') as HTMLDivElement;
    const amountOfFacilitatorsInput = document.getElementById('amountOfFacilitators') as HTMLInputElement;
    let amountOfFacilitators = parseInt(amountOfFacilitatorsInput.value);
    
    if (amountOfFacilitators === 0) {
        userPrint.innerHTML = '';
    }
    amountOfFacilitators++;
    amountOfFacilitatorsInput.value = amountOfFacilitators.toString();
    
    const facilitatorDiv = document.createElement('div');
    facilitatorDiv.className = "col-sm-12 col-md-6 col-lg-4 col-xl-4 py-0 position-relative";

    const randomIndex = Math.floor(Math.random() * 5) + 1; 
    const imageUrl = `https://storage.googleapis.com/phygital-public/Facilitators/mannetje_${randomIndex}.png`;
    const imageElement = document.createElement('img');
    imageElement.src = imageUrl;
    imageElement.className = "h-100 w-100";
    imageElement.alt = "Afbeelding_van_facilitator";
    facilitatorDiv.appendChild(imageElement);

    const menCardDiv = document.createElement('div');
    menCardDiv.className = "men-card border-5 border-black position-absolute d-flex align-items-center justify-content-center card-clickable";

    const usernameDiv = document.createElement('div');
    usernameDiv.className = "px-3";
    usernameDiv.textContent = user;
    menCardDiv.appendChild(usernameDiv);

    const trashButton = document.createElement('button');
    trashButton.className = "btn py-0 bi bi-trash btn-blue";
    trashButton.onclick = function() {
        if (confirm('Are you sure you want to remove this facilitator?')) {
            removeFacilitatorFromProject(user, projectId);
        }
    };
    menCardDiv.appendChild(trashButton);

    facilitatorDiv.appendChild(menCardDiv);
    userPrint.appendChild(facilitatorDiv);
}

function removeFacilitatorFromProject(userId: string, projectId: string) {
    fetch(`/api/Projects/RemoveUser`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ userId: userId, projectId: projectId })
    }).then(() => {
        
    });
}