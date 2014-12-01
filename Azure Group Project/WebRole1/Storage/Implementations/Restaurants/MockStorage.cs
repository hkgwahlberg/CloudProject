using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using GroupProjectWeb.Storage.Contracts;
using GroupProjectWeb.Models.Restaurant;
using GroupProjectWeb.Models.Review;

namespace GroupProjectWeb.Storage.Implementations.Restaurants
{
    public class MockStorage : IRestaurantStorage
    {
        List<RestaurantViewModel> restaurants;

        public MockStorage()
        {
            var reviews = new List<ReviewViewModel> { 
             new ReviewViewModel { RestaurantReviewId = 1, PostedDate = DateTime.Now, RestaurantName = "Restaurant 1", Reviewer = "Reviewer 1", Grade = 1, Description = "Skitkasst" },
             new ReviewViewModel { RestaurantReviewId = 2, PostedDate = DateTime.Now, RestaurantName = "Restaurant 1", Reviewer = "Reviewer 2", Grade = 3, Description = "OK" },
             new ReviewViewModel { RestaurantReviewId = 3, PostedDate = DateTime.Now, RestaurantName = "Restaurant 1", Reviewer = "Reviewer 3", Grade = 5, Description = "Skirbra" },
             new ReviewViewModel { RestaurantReviewId = 4, PostedDate = DateTime.Now, RestaurantName = "Restaurant 2", Reviewer = "Reviewer 1", Grade = 2, Description = "Skitkasst" },
             new ReviewViewModel { RestaurantReviewId = 5, PostedDate = DateTime.Now, RestaurantName = "Restaurant 2", Reviewer = "Reviewer 2", Grade = 4, Description = "Skitbra" },
             new ReviewViewModel { RestaurantReviewId = 6, PostedDate = DateTime.Now, RestaurantName = "Restaurant 2", Reviewer = "Reviewer 3", Grade = 5, Description = "Skitbra" },
             new ReviewViewModel { RestaurantReviewId = 7, PostedDate = DateTime.Now, RestaurantName = "Restaurant 3", Reviewer = "Reviewer 1", Grade = 1, Description = "Skitkasst" },
             new ReviewViewModel { RestaurantReviewId = 8, PostedDate = DateTime.Now, RestaurantName = "Restaurant 3", Reviewer = "Reviewer 2", Grade = 3, Description = "OK" },
             new ReviewViewModel { RestaurantReviewId = 9, PostedDate = DateTime.Now, RestaurantName = "Restaurant 3", Reviewer = "Reviewer 3", Grade = 4, Description = "Skitbra" },
            };

            restaurants = new List<RestaurantViewModel>()
            {
                new RestaurantFullViewModel() { RestaurantId=1, Name ="Restaurant 1", Address= "Adress1", Phone="070404040", ImageThumbnail ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg", Reviews = reviews.Where(rest=>rest.RestaurantName == "Restaurant 1").ToList() },
                new RestaurantFullViewModel() { RestaurantId=2, Name ="Restaurant 2", Address= "Adress2", Phone="070404040", ImageThumbnail ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg", Reviews = reviews.Where(rest=>rest.RestaurantName == "Restaurant 2").ToList()},
                new RestaurantFullViewModel() { RestaurantId=3, Name ="Restaurant 3", Address= "Adress3", Phone="070404040", ImageThumbnail ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg", Reviews = reviews.Where(rest=>rest.RestaurantName == "Restaurant 3").ToList()},
            };
        }

        public Task AddRestaurant(RestaurantViewModel restaurant)
        {
            return Task.Run(() =>
            {
                restaurants.Add(restaurant);
            });
        }

        public Task<RestaurantViewModel> GetRestaurant(int restaurantId)
        {
            return Task.Run(() =>
            {
                var restaurant = restaurants
                    .SingleOrDefault(rest => rest.RestaurantId == restaurantId);
                return restaurant;
            });
        }

        public Task<List<RestaurantViewModel>> GetAllRestaurants()
        {
            return Task.Run(() =>
            {
                return restaurants;
            });
        }

        public Task EditRestaurant(RestaurantViewModel editedRestaurant)
        {
            return Task.Run(() =>
            {
                var restaurant = restaurants
                    .Find(rest => rest.RestaurantId == editedRestaurant.RestaurantId);
                restaurant = editedRestaurant;
            });
        }

        public Task DeleteRestaurant(int restaurantId)
        {
            return Task.Run(() =>
            {
                var restaurant = restaurants.Find(rest => rest.RestaurantId == restaurantId);
                restaurants.Remove(restaurant);
            });
        }
    }
}