
async function checkIn()
{
    var url = baseUrl + "changevendorstatus?code=" + updateVendorKey;

    var name = document.getElementById("vendor-name").value;
    var code = document.getElementById("vendor-code").value;
    var status = document.getElementById("vendor-status").value;

    var body = {
        Name: name,
        Code: code,
        Status: status,
        Location: eventLocation
    };

    await fetch(url, {body: JSON.stringify(body), method: "POST", headers: {"Content-Type": "application/json"}});
}


var urlParams = new URLSearchParams(window.location.search);
var locationEncoded = urlParams.get('location');
var eventLocation = atob(locationEncoded);

document.getElementById("location-name").innerText = eventLocation;