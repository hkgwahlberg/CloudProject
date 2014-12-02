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
        public RestaurantReview(string id)
        {
            this.PartitionKey = "Review";
            this.RowKey = id;
        }

        public RestaurantReview() { }

        public string RestaurantReviewId { get; set; }
        public string RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string Reviewer { get; set; }
        public string Description { get; set; }
        public double Grade { get; set; }
        public DateTime PostedDate { get; set; }
    }
}