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

namespace GroupProjectWeb
{
    public class StorageService : IStorage
    {
        
        private string tableConnectionString = CloudConfigurationManager.GetSetting("TableStorageConnection");

        public async Task AddRestaurant(Restaurant restaurant)
        {
            await AddOrEditStorageItem(restaurant, "restaurantNew");
        }

        public Task<Restaurant> GetRestaurant(int restaurantId)
        {
            return Task.Run(() =>
            {
                return new Restaurant();
            });
        }

        public Task<List<Restaurant>> GetAllRestaurants()
        {
            return Task.Run(() =>
            {
                return new List<Restaurant>() { };
            });
        }

        public async Task EditRestaurant(Restaurant restaurant)
        {
            await AddOrEditStorageItem(restaurant, "restaurantEdit");
        }

        public Task DeleteRestaurant(int restaurantId)
        {
            return Task.Run(() =>
            {
                
            });
        }

        private Task AddOrEditStorageItem<T>(T itemToStorage, string type)
        {
            return Task.Run(() =>
            {
                var bMessage = new BrokeredMessage(itemToStorage, new DataContractSerializer(typeof(T)));
                bMessage.Properties["type"] = type;
                var client = ServiceBusQueueHelper.Client;
                ServiceBusQueueHelper.Client.Send(bMessage);
            });
        }
    }
}