let url = 'http://localhost:5225/api/event/';
function SearchEventFromIdClick()
{
    const id = document.getElementById('myInput').value;
    console.log(url)
    let NewUrl = url + id;
    console.log(NewUrl)
    fetch(NewUrl)
        .then(response => response.json())
        .then(event => {
            let listContainer = document.getElementById('listOfEvents');
            listContainer.innerHTML = "";
            const eventDiv = document.createElement('div');
            eventDiv.id = `event1`;
            eventDiv.innerText = `
            ID: ${event.id}
            Name: ${event.name}
            Start Date: ${event.startDate}
            End Date: ${event.endDate}
            Max Registrations: ${event.maxRegistrations}
            Location: ${event.location}
`           .trim(); // .trim() removes leading whitespace

            listContainer.appendChild(eventDiv);
        })
        .catch(error => {
            console.error('Error fetching events:', error);
        });
    NewUrl = url + id +"/numberOfUsers";
    console.log(NewUrl)
    fetch(NewUrl)
        .then(response => response.json())
        .then(numberOfUsers => {
            let event = document.getElementById('event1');
            event.innerText = event.innerText + "\n" + "the current number of users :" + numberOfUsers ;
        })
        .catch(error => {
            console.error('Error fetching events:', error);
        });

}
function deletEvent() {
    const id = document.getElementById('myInput').value;

    let deleteUrl = url + id;
    fetch(deleteUrl, {
        method: 'DELETE'
    })
        .then(response => response.text())
        .then(result => {
            document.getElementById("information").innerText = result;
            console.log(result);
        })
        .catch(error => {
            console.error('Error deleting event:', error);
            document.getElementById("information").innerText = "Error deleting event.";
        });
}
function updateEvent() {
    const eventId = document.getElementById('myInput').value;
    const newMax = parseInt(document.getElementById('numberOfUsersUpdate').value);
    const getUrl = url + eventId;
    const putUrl = getUrl;

    fetch(getUrl)
        .then(response => response.json())
        .then(existingEvent => {
            if (newMax < existingEvent.maxRegistrations) {
                console.log("You can't make the event smaller.");
                document.getElementById("information").innerText = "You can't make the event smaller.";
            } else {
                existingEvent.maxRegistrations = newMax;
                fetch(putUrl, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(existingEvent)
                })
                    .then(result => result.text())
                    .then(message => {
                        console.log("Update URL:", putUrl);
                        document.getElementById("information").innerText = message;
                    })
                    .catch(error => {
                        console.error('Error updating event:', error);
                        document.getElementById("information").innerText = "Error updating event.";
                    });
            }
        });
}
