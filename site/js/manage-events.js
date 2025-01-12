var vendorPopupEventId = "";

async function addEvent()
{
    var url = baseUrl + "addnewevent?code=" + addEventKey;

    var name = document.getElementById("event-name").value;
    var eventLocation = document.getElementById("event-location").value;

    var body = {
        Name: name,
        Location: eventLocation
    };

    await fetch(url, {body: JSON.stringify(body), method: "POST", headers: {"Content-Type": "application/json"}});
}

async function addVendorToEvent()
{
    var url = baseUrl + "addvendor?code=" + addVendorKey;

    var name = document.getElementById("vendor-name").value;

    var body = {
        Name: name,
        EventId: vendorPopupEventId
    };

    await fetch(url, {body: JSON.stringify(body), method: "POST", headers: {"Content-Type": "application/json"}});

    var vendorForm = document.getElementById("vendor-pop-up");
    vendorForm.classList.add("hidden");

    document.getElementById("vendor-event-name").value = "";
    vendorPopupEventId = "";
}

function cancelAddVendor()
{
    var vendorForm = document.getElementById("vendor-pop-up");
    vendorForm.classList.add("hidden");

    document.getElementById("vendor-event-name").value = "";
    vendorPopupEventId = "";   
}

function vendorPopUp(eventName, eventId)
{    
    document.getElementById("vendor-event-name").value = eventName;

    vendorPopupEventId = eventId;

    var vendorForm = document.getElementById("vendor-pop-up");
    vendorForm.classList.remove("hidden");
}

async function getEvents()
{
    var url = baseUrl + "listevents?code=" + listEventKey;
    var result = await fetch(url);
    return await result.json();
}

function displayEvents(events)
{
    var display = document.getElementById("event-list");

    events.forEach(event => {
        var row = document.createElement("tr");

        var nameCell = document.createElement("td");
        nameCell.innerText = event.Name;
        row.appendChild(nameCell);

        var link = document.createElement("a");
        link.href = "./list-vendors.html?event-id=" + event.Id;
        link.innerText = event.Id;

        var idCell = document.createElement("td");
        idCell.appendChild(link);
        row.appendChild(idCell);

        var locationCell = document.createElement("td");
        locationCell.innerText = event.Location; 
        row.appendChild(locationCell);

        var addVendorButton = document.createElement("button");
        addVendorButton.innerText = "Add";
        addVendorButton.onclick = function() { vendorPopUp(event.Name, event.Id); };

        var addVendorCell = document.createElement("td");
        addVendorCell.appendChild(addVendorButton);
        row.appendChild(addVendorCell);

        display.appendChild(row);
    });
}

getEvents().then(events => {
    displayEvents(events);
}).catch(err => console.log(err));
