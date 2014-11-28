using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class AzureStorageHelper
    {
        private const string _storageName = "azuregroupproject";

        public static string StorageName { get { return _storageName; } }

        public static void Initialize()
        {
            string tableConnectionString = CloudConfigurationManager.GetSetting(_storageName);
        }

        public static async Task InitializeAzureTable(string tableName)
        {
            CloudTable table = await GetAzureTable(tableName);
            table.CreateIfNotExists();

        }

        public static async Task<CloudTable> GetAzureTable(string tableName)
        {
            //TODO: Logik för att sätta upp en table
           
            return null;
        }

        public static async Task<T> GetEntityFromStorage<T>(string tableName, string partitionKey, string rowKey) where T : TableEntity
        {
            var table = await AzureStorageHelper.GetAzureTable(tableName);
            var retriveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            var retrievedResult = await table.ExecuteAsync(retriveOperation);

            return (T)retrievedResult.Result;
        }

        public static async Task<List<T>> GetEntitiesFromStorage<T>(string tableName) where T : TableEntity, new()
        {
            var table = await AzureStorageHelper.GetAzureTable(tableName);
            var query = new TableQuery<T>();
            var result = table.ExecuteQuery(query);

            return result.ToList();
        }
    }
}
