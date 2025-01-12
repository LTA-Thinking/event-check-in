
using ltat.eventManagement.models;
using ltat.eventManagement.util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ltat.eventManagement
{
    public class ListEvents
    {
        private readonly ILogger<ListEvents> _logger;

        public ListEvents(ILogger<ListEvents> logger)
        {
            _logger = logger;
        }

        [Function("ListEvents")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            var client = TableClientFactory.CreateTableClient("Events");

            var results = client.QueryAsync<EventTable>(evt => evt.PartitionKey == "event");
            List<EventOutput> eventList = new();

            await foreach (EventTable evt in results)
            {
                eventList.Add(new EventOutput(evt.Name, evt.Location, evt.RowKey));
            }

            string response = JsonSerializer.Serialize(eventList);

            return new OkObjectResult(response);
        }
    }
    public record EventOutput(string Name, string Location, string Id);
}
