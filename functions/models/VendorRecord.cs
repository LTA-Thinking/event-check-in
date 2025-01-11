using Azure;
using Azure.Data.Tables;

namespace ltat.eventManagement.models
{
    public class VendorRecord : ITableEntity
    {
        public string Name { get; set; }
        public string EventId { get; set; }
        public string Location { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public static string NewCode()
        {
            // Copied from Stack Overflow: https://stackoverflow.com/a/1344258
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}