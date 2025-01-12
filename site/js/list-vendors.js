
async function getVendors(eventId)
{
    var url = baseUrl + "listvendors?code=" + listVendorKey + "&event-id=" + eventId;
    var result = await fetch(url);
    return await result.json();
}

function displayVendors(vendors)
{
    var display = document.getElementById("vendor-list");

    vendors.forEach(vendor => {
        var row = document.createElement("tr");

        var nameCell = document.createElement("td");
        nameCell.innerText = vendor.Name;
        row.appendChild(nameCell);

        var codeCell = document.createElement("td");
        codeCell.innerText = vendor.Code;
        row.appendChild(codeCell);

        var locationCell = document.createElement("td");
        locationCell.innerText = vendor.Location; 
        row.appendChild(locationCell);

        var statusCell = document.createElement("td");
        statusCell.innerText = vendor.Status;
        row.appendChild(statusCell);

        display.appendChild(row);
    });
}

async function getVendorsForEvent()
{
    var eventId = document.getElementById("event-id").value;
    var vendors = await getVendors(eventId);
    displayVendors(vendors);
}
