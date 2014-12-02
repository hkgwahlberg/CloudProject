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
        private static string tableConnectionString = CloudConfigurationManager.GetSetting("azuregroupproject");

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

        public static async Task<bool> PostToStorage<T>(T entity, string tableName) where T : TableEntity, new()
        {
            var table = await AzureStorageHelper.GetAzureTable(tableName);
            var insertOperation = TableOperation.InsertOrReplace(entity);
            var result = table.Execute(insertOperation);

            if (result.HttpStatusCode == 204)
            {
                return true;
            }

            return false;
        }

        public static async Task<bool> DeleteFromStorage<T>(string tableName, string partitionKey, string rowKey) where T : TableEntity
        {
            var table = await AzureStorageHelper.GetAzureTable(tableName);
            var entityFromStorage = await GetEntityFromStorage<T>(tableName, partitionKey, rowKey);

            //TODO: Delete reviews for restaurant

            if (entityFromStorage != null)
            {
                var removeOperation = TableOperation.Delete(entityFromStorage);
                var result = table.Execute(removeOperation);

                if (result.HttpStatusCode == 204)
                {
                    return true;
                }
            }
            return false;
        }

        public async static Task<List<RestaurantReview>> GetReviewsForRestaurant(string restaurantId)
        {
            var table = await AzureStorageHelper.GetAzureTable("Reviews");
            var query = new TableQuery<RestaurantReview>()
                .Where(TableQuery.GenerateFilterCondition("RestaurantId", QueryComparisons.Equal, restaurantId));
            var retrievedResult = table.ExecuteQuery(query);

            return retrievedResult.ToList();
        }
    }
}
