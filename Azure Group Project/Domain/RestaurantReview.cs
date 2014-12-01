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
        public Restaurant Restaurant { get; set; }
        public string Reviewer { get; set; }
        public string Review { get; set; }
        public double Grade { get; set; }
        public DateTime PostedDate { get; set; }
    }
}