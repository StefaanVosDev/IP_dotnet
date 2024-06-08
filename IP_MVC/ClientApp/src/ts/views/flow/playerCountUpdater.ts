window.onload = function() {
    // Get the current state of the player count
    let playerCount = (document.getElementById('playerToggle') as HTMLInputElement).checked ? 2 : 1;

    // Call the updatePlayerCount function with the current state
    updatePlayerCount(playerCount);
};

function updatePlayerCount(playerCount: number) {
    console.log('Player count before fetch: ' + playerCount);

    // Replace 'url' with the actual URL or a variable that contains the URL
    let url = 'url';

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ playerCount: playerCount })
    })
        .then(response => response.json())
        .then(data => {
            console.log('Response data: ', data);
            console.log('Player count set to ' + playerCount);
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}