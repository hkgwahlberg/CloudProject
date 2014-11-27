using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class ServiceBusQueueHelper
    {
        private const string _queueName = "cloudtesthsahlberg";
        private static QueueClient _client;

        public static QueueClient Client { get { return _client; } }
        public static string QueueName { get { return _queueName; } }

        public static void Initialize()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.QueueExists(_queueName))
            {
                namespaceManager.CreateQueue(_queueName);
            }

            _client = QueueClient.CreateFromConnectionString(connectionString, _queueName);
        }
    }
}
