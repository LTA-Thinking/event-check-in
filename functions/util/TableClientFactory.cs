using Azure.Identity;
using Azure.Data.Tables;

namespace ltat.eventManagement.util
{
    public class TableClientFactory
    {
        public static TableClient CreateTableClient(string tableName)
        {
            DefaultAzureCredential credential = new();

            TableServiceClient serviceClient = new(
                endpoint: GetTableUri(),
                credential
            );

            return serviceClient.GetTableClient(
                tableName: tableName
            );
        }

        private static Uri GetTableUri()
        {
            var uriString = System.Environment.GetEnvironmentVariable("EventStorage__tableServiceUri", EnvironmentVariableTarget.Process);
            return new Uri(uriString);
        }
    }
}