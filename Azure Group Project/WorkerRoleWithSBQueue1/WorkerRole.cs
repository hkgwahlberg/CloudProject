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

namespace GroupProjectWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        private static QueueClient client;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");

            // Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
            client.OnMessage((receivedMessage) =>
                {
                    try
                    {
                        // Process the message
                        Trace.WriteLine("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());

                        // TODO: Ta emot objekt/meddelande                        

                        // TODO: skapa table klient och table från/via AzureStorageHelper

                        /* TODO: logik för att uppdatera de olika tabellerna. Följande ska kunna hanteras:
                        - Lägga till restaurang,
                        - Uppdatera restaurang,
                        - Ta bort restaurang,
                        - Lägga till review,
                        - Uppdatera review,
                        - Ta bort review
                         */

                    }
                    catch
                    {
                        // Handle any message processing specific exceptions here
                    }
                });

            CompletedEvent.WaitOne();
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
