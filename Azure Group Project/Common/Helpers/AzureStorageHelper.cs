using Domain;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
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

        //  public static string StorageName { get { return _storageName; } }

        private static string tableConnectionString = CloudConfigurationManager.GetSetting(_storageName);


        //public static async Task InitializeAzureTable(string tableName)
        //{
        //    CloudTable table = await GetAzureTable(tableName);
        //    table.CreateIfNotExists();
        //}

        private static Task<CloudTable> GetAzureTable(string tableName)
        {
            return Task.Run(() =>
            {
                var storageAccount = CloudStorageAccount.Parse(tableConnectionString);
                var tableClient = storageAccount.CreateCloudTableClient();
                var table = tableClient.GetTableReference(tableName);
                table.CreateIfNotExists();

                return table;
            });
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

        public static async Task<bool> PostEntityToStorage<T>(T entity, string tableName) where T : TableEntity, new()
        {
            var table = await AzureStorageHelper.GetAzureTable(tableName);
            var insertOperation = TableOperation.InsertOrReplace(entity);
            var result = table.Execute(insertOperation);
            //Check status
            return true;
        }

        public static async Task<bool> DeleteEntityFromStorage<T>(string partitionKey, string rowKey, string tableName) where T : TableEntity
        {
            var table = await AzureStorageHelper.GetAzureTable(tableName);
            var entityFromStorage = await GetEntityFromStorage<T>(tableName, partitionKey, rowKey);
            if (entityFromStorage != null)
            {
                var removeOperation = TableOperation.Delete(entityFromStorage);
                table.Execute(removeOperation);
            }

            //check status
            return true;
        }
    }
}
