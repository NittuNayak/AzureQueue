using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;

namespace AzureQueue
{
    class Program
    {
        static string connectionString = "DefaultEndpointsProtocol=https;AccountName=azfunctionblobstrg;AccountKey=m1n/Wdgl3ZwCl6Gl7vuaX6WenE1ipl1dAktQUYYXqXYY9e3bBDENbgE6MgD/r8Vt2bdCrGzshUw8Zq+4wm8Xlg==;";
        static string queueName = "azmknqueue";
        static void Main(string[] args)
        {
            //InsertMessageQueue("This is demo message");
            //PeekMessageFromQueue();
            //UpdateMessageInQueue();
            //PeekMessageFromQueue();
            DequeueMessageInQueue();
        }


        /// <summary>
        /// Insert Message into Queue
        /// </summary>
        /// <param name="message"></param>
        public static void InsertMessageQueue(string message)
        {
            QueueServiceClient queueServiceClient = new QueueServiceClient(connectionString);
            QueueClient queueClient = queueServiceClient.GetQueueClient(queueName);
            queueClient.CreateIfNotExists();

            if(queueClient.Exists())
            {
                queueClient.SendMessage(message);
            }

            // Instantiate a QueueClient which will be used to create and manipulate the queue
            //QueueClient queueClient = new QueueClient(connectionString, queueName);
        }

        /// <summary>
        /// This Function will read meassage from the queue 
        /// </summary>
        public static void PeekMessageFromQueue()
        {
            QueueServiceClient queueServiceClient = new QueueServiceClient(connectionString);
            QueueClient queueClient = queueServiceClient.GetQueueClient(queueName);
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                PeekedMessage[] peekedMessage = queueClient.PeekMessages();                
                Console.WriteLine($"Peeked message: '{peekedMessage[0].MessageText}'");
            }
        }

        /// <summary>
        /// This Function will Update Message
        /// </summary>
        public static void UpdateMessageInQueue()
        {
            QueueServiceClient queueServiceClient = new QueueServiceClient(connectionString);
            QueueClient queueClient = queueServiceClient.GetQueueClient(queueName);
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                QueueMessage[] queueMessages = queueClient.ReceiveMessages();
                queueClient.UpdateMessage(queueMessages[0].MessageId,
                    queueMessages[0].PopReceipt,
                    "Message has been Updated",
                    TimeSpan.FromSeconds(60.0));
            }
        }

        /// <summary>
        /// This Function will dequemsg
        /// </summary>
        public static void DequeueMessageInQueue()
        {
            QueueServiceClient queueServiceClient = new QueueServiceClient(connectionString);
            QueueClient queueClient = queueServiceClient.GetQueueClient(queueName);
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                QueueMessage[] queueMessages = queueClient.ReceiveMessages();
                queueClient.DeleteMessage(queueMessages[0].MessageId, queueMessages[0].PopReceipt);
            }
        }
    }
}
