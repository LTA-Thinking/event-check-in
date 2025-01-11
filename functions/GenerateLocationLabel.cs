using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using QRCoder;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace ltat.eventManagement
{
    public class GenerateLocationLabel
    {
        private readonly ILogger<GenerateLocationLabel> _logger;

        public GenerateLocationLabel(ILogger<GenerateLocationLabel> logger)
        {
            _logger = logger;
        }

        [Function("GenerateLocationLabel")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, [FromBody] Location location)
        {
            string baseUrl = System.Environment.GetEnvironmentVariable("QRCode_baseUrl", EnvironmentVariableTarget.Process) ?? string.Empty;
            string encodedLocation = Base64UrlTextEncoder.Encode(Encoding.UTF8.GetBytes(location.Name));
            string url = baseUrl + encodedLocation;

            // QR code generator: https://github.com/codebude/QRCoder/tree/master
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                return new OkObjectResult(qrCodeImage);
            }
        }
    }

    public record Location(string Name);
}