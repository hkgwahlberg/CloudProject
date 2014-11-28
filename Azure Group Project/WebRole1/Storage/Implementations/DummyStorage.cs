using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Domain;
using GroupProjectWeb.Storage.Contracts;

namespace GroupProjectWeb.Storage.Implementations
{
    public class DummyStorage : IStorage
    {
        List<Restaurant> dummyRestaurants;

        public DummyStorage()
        {
            dummyRestaurants = new List<Restaurant>()
            {
                new Restaurant() { RestaurantId=1, Name ="Restaurant 1", Address= "Adress1", Phone="070404040", ImageURL ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg"},
                new Restaurant() { RestaurantId=2, Name ="Restaurant 2", Address= "Adress2", Phone="070404040", ImageURL ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg"},
                new Restaurant() { RestaurantId=3, Name ="Restaurant 3", Address= "Adress3", Phone="070404040", ImageURL ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg"},
            };
        }

        public Task AddRestaurant(Restaurant restaurant)
        {
            return Task.Run(() =>
            {
                dummyRestaurants.Add(restaurant);
            });
        }

        public Task<Restaurant> GetRestaurant(int restaurantId)
        {
            return Task.Run(() =>
            {
                var restaurant = dummyRestaurants
                    .SingleOrDefault(rest => rest.RestaurantId == restaurantId);
                return restaurant;
            });
        }

        public Task<List<Restaurant>> GetAllRestaurants()
        {
            return Task.Run(() =>
            {
                return dummyRestaurants;
            });
        }

        public Task EditRestaurant(Restaurant restaurant)
        {
            return Task.Run(() =>
            {
                var dummyRestaurant = dummyRestaurants
                    .Find(rest => rest.RestaurantId == restaurant.RestaurantId);
                dummyRestaurant = restaurant;
            });
        }

        public Task DeleteRestaurant(int restaurantId)
        {
            return Task.Run(() =>
            {
                var restaurant = dummyRestaurants.Find(rest => rest.RestaurantId == restaurantId);
                dummyRestaurants.Remove(restaurant);
            });
        }
    }
}