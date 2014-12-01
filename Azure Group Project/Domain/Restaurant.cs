using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;
using Microsoft.WindowsAzure.Storage.Table;
using Common.Helpers;

namespace Domain
{
    public class Restaurant : TableEntity
    {
        public Restaurant(int id)
        {
            this.PartitionKey = "Restaurant";
            this.RowKey = id.ToString();
        }

        public Restaurant()
        { }

        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ImageURL { get; set; }
        public string ImageThumbnail { get; set; }
        public string Phone { get; set; }
    }
}
