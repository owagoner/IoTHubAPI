using IoTHubAPI.Models;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTHubAPI.Serivces
{
    public class ServiceBus
    {

        private string connectionString = "Endpoint=sb://farmiot.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=mAxLq2xmPeZAWrHU6Q9QeWV7oFySuxoRBRsuU4VuTT8=";
        private QueueClient client;

        public long getNumberOfMessages(string queue) {
            NamespaceManager nm = NamespaceManager.CreateFromConnectionString(connectionString);
            return nm.GetQueue(queue).MessageCount;
        }

        public async System.Threading.Tasks.Task<bool> deleteQueueMessagesAsync(string queue)
        {
            //NamespaceManager nm = NamespaceManager.CreateFromConnectionString(connectionString);
            var mf = MessagingFactory.CreateFromConnectionString(connectionString);
            var receiver = await mf.CreateMessageReceiverAsync(queue, ReceiveMode.ReceiveAndDelete);

            NamespaceManager nm = NamespaceManager.CreateFromConnectionString(connectionString);
            int numMessages = (int) nm.GetQueue(queue).MessageCount;
             

            while (true)
            {
                var messages = await receiver.ReceiveBatchAsync(numMessages);
                if (!messages.Any())
                {
                    break;
                }
            };

            return true;
        }

    }
}