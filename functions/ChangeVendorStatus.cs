using ltat.eventManagement.util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace ltat.eventManagement
{
    public class ChangeVendorStatus
    {
        private readonly ILogger<ChangeVendorStatus> _logger;

        public ChangeVendorStatus(ILogger<ChangeVendorStatus> logger)
        {
            _logger = logger;
        }

        [Function("ChangeVendorStatus")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, [FromBody] VendorStatus status)
        {
            if (!await TableClientHelpers.UpdateVendor(status.Name, status.Code, status.Location, status.Status))
            {
                return new BadRequestObjectResult("Invalid name and code");
            }

            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }

    public record VendorStatus(string Name, string Code, string Location, string Status);
}
