let url = 'http://localhost:5225/api/event/';


function SearchEventClick() {
const value = document.getElementById('myInput').value;
    let locationUrl = url + value + "/eventLocation"
    console.log(locationUrl)
    fetch(locationUrl)
    .then(response => response.json())
    .then(events =>
    {
        let listContainer = document.getElementById('listOfEvents');
        listContainer.innerHTML = "";
        events.forEach((event, index) => {
            const eventDiv = document.createElement('div');
            eventDiv.id = `event${index + 1}`;
            eventDiv.innerText = `
            ID: ${event.id}
            Name: ${event.name}
            Start Date: ${event.startDate}
            End Date: ${event.endDate}
            Max Registrations: ${event.maxRegistrations}
            Location: ${event.location}
`   .trim(); // .trim() removes leading whitespace
            const encodedLocation = encodeURIComponent(event.location);
            link=document.createElement("a");
            link.href = "https://www.google.com/maps/search/?api=1&query="+encodedLocation;
            link.innerText = "link to google maps of the location : "+event.location;



            listContainer.appendChild(eventDiv);
            listContainer.appendChild(link);

        });
    })
    .catch(error => {
        console.error('Error fetching events:', error);
    });
}
function AddUser()
{
    const name = document.getElementById("nameInput").value;
    const birthday = document.getElementById("birsdayInput").value;
    const eventId = document.getElementById("idInput").value;
    const newUser = {
        name: name,
        dateOfBirth: birthday 
    };
    const postUrl = url + eventId + "/registration";
    console.log(postUrl)

    fetch(postUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newUser)
    })
        .then(response => response.text()) // since controller returns ActionResult<string>
        .then(result => {
            document.getElementById("information").innerText = result;

        })
        .catch(error => {
            console.error("Error adding user:", error);
            document.getElementById("information").innerText = "Error adding user.";
        });
}