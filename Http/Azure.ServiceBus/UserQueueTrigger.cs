using System;
using System.Threading.Tasks;
using Azure.ServiceBus.DTOs;
using Azure.ServiceBus.Models;
using Azure.ServiceBus.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Azure.ServiceBus
{
    public class UserQueueTrigger
    {
        private readonly IUserService _userService; 

        public UserQueueTrigger(IUserService userService)
        {
            _userService = userService;
        }

        [FunctionName("UserQueueTrigger")]
        public async Task Run([ServiceBusTrigger("users", Connection = "ServiceBusConnectionString")]string myQueueItem, ILogger log)
        {
            QueueDTO queueDTO = JsonConvert.DeserializeObject<QueueDTO>(myQueueItem) ?? new QueueDTO();
            dynamic payLoad = queueDTO.PayLoad;

            switch(queueDTO.Operation)
            {
                case "Add":
                    var addResponse = await _userService.AddUserToDBFromQueue(payLoad);
                    log.LogInformation(addResponse ? "User Added" : "User not added");
                    break;
                case "Update":
                    var updateResponse = await _userService.UpdateUserToDBFromQueue(payLoad);
                    log.LogInformation(updateResponse ? "User Updated" : "User not updated");
                    break;
                case "Delete":
                    var deleteResponse = await _userService.DeleteUserFromDbReceivedFromQueue(Convert.ToInt32(payLoad.id));
                    log.LogInformation(deleteResponse ? "User Updated" : "User not updated");
                    break;
            }
        }
    }
}
