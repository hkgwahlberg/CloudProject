using GroupProjectWeb.Models.Review;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectWeb.Models.Restaurant
{
    public class RestaurantFullViewModel : RestaurantViewModel
    {
        public RestaurantFullViewModel()
        {
            Reviews = new List<ReviewViewModel>();
        }

        public List<ReviewViewModel> Reviews { get; set; }
    }
}
