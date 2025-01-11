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
            var client = TableClientFactory.CreateTableClient("Vendors");

            var results = client.QueryAsync<VendorRecord>(evt => evt.PartitionKey == "vendor");
            List<VendorRecord> vendorList = new();

            await foreach (VendorRecord vnd in results)
            {
                vendorList.Add(vnd);
            }

            string response = JsonSerializer.Serialize(vendorList);

            return new OkObjectResult(response);
        }
    }
}
