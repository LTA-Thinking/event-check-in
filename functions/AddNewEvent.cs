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
    public class AddNewEvent
    {
        private readonly ILogger<AddNewEvent> _logger;

        public AddNewEvent(ILogger<AddNewEvent> logger)
        {
            _logger = logger;
        }

        [Function("AddNewEvent")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, [FromBody] Event evt)
        {
            var eventRecord = new EventTable()
            {
                Name = evt.name,
                Location = evt.location,
                PartitionKey = "event",
                RowKey = Guid.NewGuid().ToString(),
                Timestamp = DateTimeOffset.UtcNow,
                ETag = new ETag("1")
            };

            await TableClientHelpers.AddRowToTable("Events", eventRecord);

            return new OkObjectResult("Success!");
        }
    }

    public record Event(string name, string location);//, DateTime startDate, DateTime endDate);
}
