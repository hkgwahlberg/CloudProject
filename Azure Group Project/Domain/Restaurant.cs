using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;
using Microsoft.WindowsAzure.Storage.Table;

namespace Domain
{
    public class Restaurant : TableEntity
    {
        public Restaurant(string id)
        {
            this.PartitionKey = "Restaurant";
            this.RowKey = id;
        }

        public Restaurant() { }

        public string RestaurantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ImageURL { get; set; }
        public string ImageThumbnail { get; set; }
        public string Phone { get; set; }
    }
}
