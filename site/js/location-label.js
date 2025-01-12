
async function generateQrCode()
{
    var location = document.getElementById("location-input").value;

    var url = baseUrl + "generatelocationlabel?code=" + generateQrCodeKey + "&location=" + location;
    var result = await fetch(url);
    var qrCode = await result.text();
    qrCode = qrCode.substring(1, qrCode.length-1);

    var display = document.getElementById("qr-code");
    display.src = "data:image/png;base64," + qrCode;

    var label = document.getElementById("qr-code-label");
    label.innerText = location;
}