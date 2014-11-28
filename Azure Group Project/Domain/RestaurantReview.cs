using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain
{
    public class RestaurantReview : TableEntity
    {
        public RestaurantReview(int id)
        {
            this.PartitionKey = "Review";
            this.RowKey = id.ToString();
        }

        public RestaurantReview()
        { }

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