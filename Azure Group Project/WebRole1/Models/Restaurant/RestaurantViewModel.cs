using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GroupProjectWeb.Models.Restaurant
{
    public class RestaurantViewModel
    {
        public int RestaurantId { get; set; }
        [StringLength(100), Required]
        public string Name { get; set; }
        [StringLength(100), Required]
        public string Address { get; set; }
        [StringLength(1000)]        
        public string ImageURL { get; set; }
        [DisplayName("Image")]
        public string ImageThumbnail { get; set; }
        [StringLength(12)]
        public string Phone { get; set; }
    }
}
