using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class AzureStorageHelper
    {
        private const string _storageName = "azuregroupproject";

        public static string StorageName { get { return _storageName; } }

        public static void Initialize()
        {
            string tableConnectionString = CloudConfigurationManager.GetSetting("azuregroupproject");
        }

        public static void InitializeAzureTable(string tableName)
        {
            CloudTable table = GetAzureTable(tableName);
            table.CreateIfNotExists();
        }

        private static CloudTable GetAzureTable(string tableName)
        {
            //TODO: Logik för att sätta upp en table 

            return null;
        }
    }
}
