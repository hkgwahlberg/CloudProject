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
using GroupProjectWeb.Models.Restaurant;
using AutoMapper;
namespace GroupProjectWeb.Storage.Implementations
{
    public class StorageService : IRestaurantStorage
    {
        private string tableConnectionString;

        public StorageService()
        {
            tableConnectionString = CloudConfigurationManager.GetSetting("TableStorageConnection");

            Mapper.CreateMap<Restaurant, RestaurantViewModel>();
            Mapper.CreateMap<RestaurantViewModel, Restaurant>();
            Mapper.CreateMap<RestaurantFullViewModel, Restaurant>();
        }
        public async Task AddRestaurant(RestaurantViewModel restaurantFromView)
        {
            var restaurant = Mapper.Map<Restaurant>(restaurantFromView);

            await AddOrUpdateItem(restaurant);
        }

        public async Task<RestaurantViewModel> GetRestaurant(int restaurantId)
        {
            var restaurant = await AzureStorageHelper.GetEntityFromStorage<Restaurant>(
                "restaurants", "restaurant", restaurantId.ToString());

            var viewModel = Mapper.Map<RestaurantFullViewModel>(restaurant);

            viewModel.Reviews = null; //insert logic for getting reviews

            return viewModel;
        }

        public async Task<List<RestaurantViewModel>> GetAllRestaurants()
        {

            var allRestaurants = await AzureStorageHelper.GetEntitiesFromStorage<Restaurant>("restaurants");

            var restaurantsToView = Mapper.Map<List<RestaurantViewModel>>(allRestaurants);

            return restaurantsToView;
        }

        public async Task EditRestaurant(RestaurantViewModel restaurantFromView)
        {
            var restaurant = Mapper.Map<Restaurant>(restaurantFromView);

            await AddOrUpdateItem(restaurant);
        }

        public Task DeleteRestaurant(int restaurantId)
        {
            return Task.Run(() =>
            {

            });
        }

        private Task AddOrUpdateItem<T>(T itemToStorage)
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