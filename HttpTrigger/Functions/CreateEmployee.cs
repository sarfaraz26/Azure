using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Data;
using HttpTrigger.Models;
using Azure.Messaging.ServiceBus;

namespace HttpTrigger.Functions
{
    public class CreateEmployee
    {
        [FunctionName("CreateEmployee")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "createemployee")] HttpRequest req,
            ILogger log)
        {
            string queueConnectionString = Environment.GetEnvironmentVariable("QueueConnectionString");
            string queueName = Environment.GetEnvironmentVariable("QueueName");
            string payloadString = await req.ReadAsStringAsync();

            ServiceBusClient serviceBusClient = new(queueConnectionString);
            var sender = serviceBusClient.CreateSender(queueName);

            ServiceBusMessage message = new ServiceBusMessage(payloadString);
            await sender.SendMessageAsync(message);

            return new OkObjectResult(string.Empty);

        }
    }
}
