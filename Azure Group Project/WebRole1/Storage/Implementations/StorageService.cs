using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;
using Domain;
using Common.Helpers;
using System.Runtime.Serialization;
using GroupProjectWeb.Storage.Contracts;
using Microsoft.WindowsAzure.Storage.Table;

namespace GroupProjectWeb.Storage.Implementations
{
    public class StorageService : IStorage
    {

        private string tableConnectionString = CloudConfigurationManager.GetSetting("TableStorageConnection");

        public async Task AddRestaurant(Restaurant restaurant)
        {
            await AddOrUpdateStorageItem(restaurant);
        }

        public async Task<Restaurant> GetRestaurant(int restaurantId)
        {
            var restaurant = await AzureStorageHelper.GetEntityFromStorage<Restaurant>(
                "restaurants", "restaurant", restaurantId.ToString());

            return restaurant;
        }

        public async Task<List<Restaurant>> GetAllRestaurants()
        {

            var allRestaurants = await AzureStorageHelper.GetEntitiesFromStorage<Restaurant>("restaurants");
            return allRestaurants;
        }

        public async Task EditRestaurant(Restaurant restaurant)
        {
            await AddOrUpdateStorageItem(restaurant);
        }

        public Task DeleteRestaurant(int restaurantId)
        {
            return Task.Run(() =>
            {

            });
        }

        private Task AddOrUpdateStorageItem<T>(T itemToStorage)
        {
            return Task.Run(() =>
            {
                var bMessage = new BrokeredMessage(itemToStorage, new DataContractSerializer(typeof(T)));
                var client = ServiceBusQueueHelper.Client;
                ServiceBusQueueHelper.Client.Send(bMessage);
            });
        }
    }
}