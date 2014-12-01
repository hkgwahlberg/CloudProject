
using GroupProjectWeb.Models.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectWeb.Storage.Contracts
{
    public interface IRestaurantStorage
    {
        Task AddRestaurant(RestaurantViewModel restaurant);
        Task<RestaurantViewModel> GetRestaurant(int restaurantId);
        Task<List<RestaurantViewModel>> GetAllRestaurants();
        Task EditRestaurant(RestaurantViewModel restaurant);
        Task DeleteRestaurant(int restaurantId);
    }
}
