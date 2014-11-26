using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GroupProjectWeb.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        [StringLength(100), Required]
        public string Name { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(1000)]
        [DisplayName("Image")]
        public string ImageURL { get; set; }
        [StringLength(12)]
        public string Phone { get; set; }
    }
}
