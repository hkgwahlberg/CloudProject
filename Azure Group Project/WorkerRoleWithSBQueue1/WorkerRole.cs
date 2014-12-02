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
                bool isSuccess = await DeleteStorageItem(type, receivedMessage);
            }
        }

        private async Task<bool> DeleteStorageItem(string type, BrokeredMessage receivedMessage)
        {
            var id = receivedMessage.GetBody<String>();
            bool isSuccess = false;

            if (type.Equals("Restaurant"))
            {
                isSuccess = await AzureStorageHelper.DeleteEntityFromStorage<Restaurant>("Restaurant", id, "Restauants");
            }
            else if (type.Equals("Review"))
            {
                isSuccess = await AzureStorageHelper.DeleteEntityFromStorage<RestaurantReview>("Review", id, "Reviews");
            }
            return isSuccess;
        }

        private async Task<bool> AddOrUpdateStorageItem(string type, string request, BrokeredMessage receivedMessage)
        {
            string table = "";

            var itemToStorage = new TableEntity();

            if (type.Equals("Restaurant"))
            {
                itemToStorage = receivedMessage.GetBody<Restaurant>();
                table = "Restaurants";
            }
            else if (type.Equals("Review"))
            {
                itemToStorage = receivedMessage.GetBody<RestaurantReview>();
                table = "Reviews";
            }

            var isSuccess = await AzureStorageHelper.PostEntityToStorage(itemToStorage, table);
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
