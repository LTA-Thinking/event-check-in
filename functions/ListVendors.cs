using System.Text.Json;
using ltat.eventManagement.models;
using ltat.eventManagement.util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ltat.eventManagement
{
    public class ListVendors
    {
        private readonly ILogger<ListVendors> _logger;

        public ListVendors(ILogger<ListVendors> logger)
        {
            _logger = logger;
        }

        [Function("ListVendors")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            string eventId = req.Query["event-id"];

            var client = TableClientFactory.CreateTableClient("Vendors");

            var results = client.QueryAsync<VendorRecord>(vnd => vnd.PartitionKey == "vendor" && vnd.EventId == eventId);
            List<VendorOutput> vendorList = new();

            await foreach (VendorRecord vnd in results)
            {
                vendorList.Add(new VendorOutput(vnd.Name, vnd.Location, vnd.Status, vnd.Code));
            }

            string response = JsonSerializer.Serialize(vendorList);

            return new OkObjectResult(response);
        }
    }
    public record VendorOutput(string Name, string Location, string Status, string Code);
}
