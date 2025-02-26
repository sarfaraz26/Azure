using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.CRUD.Services;

namespace Azure.CRUD.Functions.Http
{
    public class GetUsers
    {
        private readonly IUserService _userService;

        public GetUsers(IUserService userService)
        {
            _userService = userService;
        }

        [FunctionName("GetUsers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, ILogger log)
        {
            try
            {
                string id = req.Query["id"];
                var response = await _userService.GetUsersAsync(id);
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
