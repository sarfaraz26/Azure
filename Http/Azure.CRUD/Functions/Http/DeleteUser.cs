using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.CRUD.DTOs;
using Azure.CRUD.Services;

namespace Azure.CRUD.Functions.Http
{
    public class DeleteUser
    {
        private readonly IUserService _userService;
        
        public DeleteUser(IUserService userService)
        {
            _userService = userService;    
        }

        [FunctionName("DeleteUser")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteUser/{id}")] HttpRequest req, 
            int id, ILogger log)
        {
            try
            {
                var response = await _userService.SendDeleteUserObjectToQueueAsync(id);
                return new OkObjectResult(response);
            }
            catch(Exception ex)
            {
                log.LogError($"Exception {ex.Message} occured at {ex.StackTrace}");
                return new BadRequestResult();
            }
        }
    }
}
