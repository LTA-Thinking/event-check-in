using Azure.Data.Tables;
using ltat.eventManagement.models;
using Microsoft.AspNetCore.Http;

namespace ltat.eventManagement.util
{
    public class TableClientHelpers
    {
        public static async Task AddRowToTable<T>(string tableName, T record)
        where T: ITableEntity
        {
            var client = TableClientFactory.CreateTableClient(tableName);

            await client.AddEntityAsync<T>(
                entity: record
            );
        }

        public static async Task<bool> UpdateVendor(string name, string code, string location, string status)
        {
            var client = TableClientFactory.CreateTableClient("Vendors");
            var results = client.QueryAsync<VendorRecord>(vnd => vnd.PartitionKey == "vendor" && vnd.Name == name && vnd.Code == code);
            
            int count = 0;
            VendorRecord vendor = new VendorRecord();
            await foreach (VendorRecord vnd in results)
            {
                vendor = vnd;
                count++;
            }

            if (count == 1)
            {
                vendor.Location = location;
                vendor.Status = status;
                
                await client.UpdateEntityAsync(vendor, vendor.ETag, TableUpdateMode.Replace);

                return true;
            }
            else if (count == 0)
            {
                return false;
            }
            else
            {
                throw new Exception("Multiple matches found");
            }
        }
    }
}






