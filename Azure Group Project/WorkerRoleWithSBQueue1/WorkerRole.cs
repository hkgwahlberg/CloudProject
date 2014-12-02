using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Common.Helpers;
using Domain;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace GroupProjectWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        private static QueueClient client;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");

            client.OnMessageAsync(async (receivedMessage) =>
                 {
                     // Process the message
                     Trace.WriteLine("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());

                     await HandleReceivedMessage(receivedMessage);
                 });

            CompletedEvent.WaitOne();
        }

        private async Task HandleReceivedMessage(BrokeredMessage receivedMessage)
        {
            var type = receivedMessage.Properties["Type"].ToString();
            var request = receivedMessage.Properties["Request"].ToString();

            if (request.Equals("Add") || request.Equals("Update"))
            {
                var isSucess = await AddOrUpdateStorageItem(type, request, receivedMessage);
            }
            else if (request.Equals("Delete"))
            {
                var id = receivedMessage.Properties["Id"].ToString();
                var isSuccess = await DeleteStorageItem(type, id);
            }
        }

        private async Task<bool> DeleteStorageItem(string type, string id)
        {
            bool isSuccess = false;

            if (type.Equals("Restaurant"))
            {
                isSuccess = await AzureStorageHelper.DeleteFromStorage<Restaurant>("Restaurants", "Restaurant", id);
            }
            else if (type.Equals("Review"))
            {
                isSuccess = await AzureStorageHelper.DeleteFromStorage<RestaurantReview>("Reviews", "Review", id);
            }
            return isSuccess;
        }

        private async Task<bool> AddOrUpdateStorageItem(string type, string request, BrokeredMessage receivedMessage)
        {
            bool isSuccess = false;
            if (type.Equals("Restaurant"))
            {
                var itemToStorage = receivedMessage.GetBody<Restaurant>();
                isSuccess = await AzureStorageHelper.PostToStorage(itemToStorage, "Restaurants");
            }
            else if (type.Equals("Review"))
            {
                var itemToStorage = receivedMessage.GetBody<RestaurantReview>();
                isSuccess = await AzureStorageHelper.PostToStorage(itemToStorage, "Reviews");
            }
            return isSuccess;
        }

        public override bool OnStart()
        {
            // Initialize the connection to Service Bus Queue
            ServiceBusQueueHelper.Initialize();
            client = ServiceBusQueueHelper.Client;
            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            client.Close();
            CompletedEvent.Set();

            base.OnStop();
        }
    }
}
