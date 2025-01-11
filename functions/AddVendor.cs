using Azure;
using ltat.eventManagement.models;
using ltat.eventManagement.util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace ltat.eventManagement
{
    public class AddVendor
    {
        private readonly ILogger<AddVendor> _logger;

        public AddVendor(ILogger<AddVendor> logger)
        {
            _logger = logger;
        }

        [Function("AddVendor")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, [FromBody] Vendor vendor)
        {
            var vendorRecord = new VendorRecord()
            {
                Name = vendor.Name,
                EventId = vendor.EventId,
                Code = VendorRecord.NewCode(),
                Location = string.Empty,
                Status = string.Empty,
                PartitionKey = "vendor",
                RowKey = Guid.NewGuid().ToString(),
                Timestamp = DateTimeOffset.UtcNow,
                ETag = new ETag("1")
            };

            await TableClientHelpers.AddRowToTable("Vendors", vendorRecord);

            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }

    public record Vendor(string EventId, string Name);
}
