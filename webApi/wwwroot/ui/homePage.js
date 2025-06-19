function homPageStart() {
    let mainDiv = document.body.querySelector("#main");

    let baseDiv = document.createElement("div");

    let header = document.createElement("h1");
    header.innerText = "Welcome to the event searcher from around the world";


    let userLink = document.createElement("a");
    userLink.href = "http://localhost:5289/userPage.html";
    userLink.style.textDecoration = "none";
    userLink.innerText = "Click here if you are a user";

    let userDiv = document.createElement("div");
    userDiv.id = "useEntryDiv";

    userDiv.appendChild(userLink);

    // Create link for manager
    let managerLink = document.createElement("a");
    managerLink.href = "http://localhost:5289/manegerPage.html";
    managerLink.innerText = "Click here if you are a manager";

    managerLink.style.textDecoration = "none";

    let managerDiv = document.createElement("div");
    managerDiv.id = "managerEntryDiv";

    managerDiv.appendChild(managerLink);


    let createEventLink = document.createElement("a");
    createEventLink.href = "http://localhost:5289/addEventPage.html";
    createEventLink.style.textDecoration = "none";
    createEventLink.innerText = "Click here if you want to add event";

    let createEventDiv = document.createElement("div");
    createEventDiv.id = "createEventDiv";

    createEventDiv.appendChild(createEventLink);

    // Add all elements
    baseDiv.appendChild(header);
    baseDiv.appendChild(userDiv);
    baseDiv.appendChild(managerDiv);
    baseDiv.appendChild(createEventDiv);

    mainDiv.appendChild(baseDiv);
}

homPageStart();
