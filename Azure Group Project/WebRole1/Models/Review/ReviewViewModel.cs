using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GroupProjectWeb.Models.Review
{
    public class ReviewViewModel
    {
        public int RestaurantReviewId { get; set; }
        [StringLength(50)]
        public string Reviewer { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        [Range(1, 5)]
        public double Grade { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostedDate { get; set; }

        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public string RestaurantName { get; set; }
    }
}
