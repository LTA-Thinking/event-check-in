using Azure;
using Azure.Data.Tables;

namespace ltat.eventManagement.models
{
    public class EventTable : ITableEntity
    {
        //public string Id { get; set; }
        public string Name { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public string Location { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set;}
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}