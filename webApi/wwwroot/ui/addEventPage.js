let url = 'http://localhost:5225/api/event';
function AddEvent() {
    const name = document.getElementById('nameInput').value;
    const startDate = document.getElementById('startDateInput').value;
    const endDate = document.getElementById('endDateInput').value;
    const maxRegistrations = parseInt(document.getElementById('maxRegistrationsInput').value);
    const location = document.getElementById('locationInput').value;

    const newEvent = {
        name: name,
        startDate: startDate,
        endDate: endDate,
        maxRegistrations: maxRegistrations,
        location: location
    };

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newEvent)
    })
        .then(response => response.text())
        .then(message => {
           
            document.getElementById("information").innerText = message ;
            } 
        )
        .catch(error => {
            console.error('Error adding event:', error);
            document.getElementById("information").innerText = "Error adding event.";
        });
}
