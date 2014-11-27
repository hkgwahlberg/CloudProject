
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectWeb
{
    public interface IStorage
    {
        Task AddRestaurant(Restaurant restaurant);
        Task<Restaurant> GetRestaurant(int restaurantId);
        Task<List<Restaurant>> GetAllRestaurants();
        Task EditRestaurant(Restaurant restaurant);
        Task DeleteRestaurant(int restaurantId);
    }
}
