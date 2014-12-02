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
using GroupProjectWeb.Models.Review;
namespace GroupProjectWeb.Storage.Implementations.Restaurants
{
    public class StorageService : IRestaurantStorage
    {
        public StorageService()
        {
            Mapper.CreateMap<Restaurant, RestaurantViewModel>();
            Mapper.CreateMap<RestaurantViewModel, Restaurant>();
            Mapper.CreateMap<RestaurantFullViewModel, Restaurant>();
            Mapper.CreateMap<Restaurant, RestaurantFullViewModel>();
            Mapper.CreateMap<RestaurantReview, ReviewViewModel>();
        }

        public async Task AddRestaurant(RestaurantViewModel restaurantFromView)
        {
            if (string.IsNullOrEmpty(restaurantFromView.RestaurantId))
            {
                restaurantFromView.RestaurantId = Guid.NewGuid().ToString();
            }

            var restaurant = new Restaurant(restaurantFromView.RestaurantId)
            {
                RestaurantId = restaurantFromView.RestaurantId,
                Address = restaurantFromView.Address,
                ImageThumbnail = restaurantFromView.ImageThumbnail,
                ImageURL = restaurantFromView.ImageURL,
                Name = restaurantFromView.Name,
                Phone = restaurantFromView.Phone
            };

            await SendToStorage(restaurant, "Add", restaurant.PartitionKey);
        }

        public async Task UpdateRestaurant(RestaurantViewModel restaurantFromView)
        {
            var restaurant = new Restaurant(restaurantFromView.RestaurantId)
            {
                RestaurantId = restaurantFromView.RestaurantId,
                Address = restaurantFromView.Address,
                ImageThumbnail = restaurantFromView.ImageThumbnail,
                ImageURL = restaurantFromView.ImageURL,
                Name = restaurantFromView.Name,
                Phone = restaurantFromView.Phone
            };

            await SendToStorage(restaurant, "Update", restaurant.PartitionKey);
        }

        public async Task<RestaurantViewModel> GetRestaurant(string restaurantId)
        {
            var restaurant = await AzureStorageHelper.GetEntityFromStorage<Restaurant>(
                "Restaurants", "Restaurant", restaurantId);

            var viewModel = Mapper.Map<RestaurantFullViewModel>(restaurant);

            var reviewsForRestaurant = await AzureStorageHelper.GetReviewsForRestaurant(restaurant.RestaurantId);

            viewModel.Reviews = Mapper.Map<List<ReviewViewModel>>(reviewsForRestaurant);
            return viewModel;
        }

        public async Task<List<RestaurantViewModel>> GetAllRestaurants()
        {
            var allRestaurants = await AzureStorageHelper.GetEntitiesFromStorage<Restaurant>("Restaurants");

            var restaurantsToView = Mapper.Map<List<RestaurantViewModel>>(allRestaurants);

            return restaurantsToView;
        }

        public async Task DeleteRestaurant(string restaurantId)
        {
            await SendToStorage(restaurantId, "Delete", "Restaurant");
        }

        private Task SendToStorage<T>(T itemToStorage, string request, string type)
        {
            return Task.Run(() =>
            {
                var bMessage = new BrokeredMessage(itemToStorage);
                bMessage.Properties["Request"] = request;
                bMessage.Properties["Type"] = type;

                if (request.Equals("Delete"))
                {
                    bMessage.Properties["Id"] = itemToStorage;
                }

                var client = ServiceBusQueueHelper.Client;
                ServiceBusQueueHelper.Client.Send(bMessage);
            });
        }
    }
}