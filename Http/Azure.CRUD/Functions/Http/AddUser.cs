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
    public class AddUser
    {
        private readonly IUserService _userService;

        public AddUser(IUserService userService)
        {
            _userService = userService;    
        }

        [FunctionName("AddUser")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var response = await _userService.SendAddUserObjectToQueueAsync(requestBody);
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
