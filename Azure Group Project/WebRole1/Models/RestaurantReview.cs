using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProjectWeb.Models
{
    public class RestaurantReview
    {
        public int RestaurantReviewId { get; set; }
        [Required]
        public Restaurant Restaurant { get; set; }
        [StringLength(50)]
        public string Reviewer { get; set; }
        [StringLength(1000), Required]
        public string Review { get; set; }
        [Required]
        public double Grade { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostedDate { get; set; }
    }
}