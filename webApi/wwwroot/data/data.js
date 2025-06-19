const string localHost="http://localhost:5225/";
function getEventThroughId(id) {
    const url = localHost + "api/event" + id;
    fatch(url)
        .then((response) => {
            return response.json();
        })
        .then((data) => {
           
        })
        .catch(function (error) {
            console.log(error);
        });
}
