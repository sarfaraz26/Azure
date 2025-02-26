using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Azure.CRUD.Resources
{
    public class Helper
    {
        #region Properties
        public static IDbConnection DbConnection
        {
            get
            {
                var connectionString = Environment.GetEnvironmentVariable("DbConnectionString") ?? string.Empty;
                return new SqlConnection(connectionString);
            }
        }

        public static string ServiceBusQueueName
        {
            get
            {
                return Environment.GetEnvironmentVariable("ServiceBusQueueName") ?? string.Empty;
            }
        }

        public static string ServiceBusConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable("ServiceBusConnectionString") ?? string.Empty;
            }
        }
        #endregion



        #region Methods
        public static async Task<string> SendMessageToQueue(string param)
        {
            string response = "Message failed to sent to queue";
            
            await using (ServiceBusClient client = new ServiceBusClient(Helper.ServiceBusConnectionString))
            {
                ServiceBusSender sender = client.CreateSender(Helper.ServiceBusQueueName);
                ServiceBusMessage message = new ServiceBusMessage(param);
                await sender.SendMessageAsync(message);
                response = "Message sent to queue";
            }

            return response;
        }
        #endregion

    }
}
