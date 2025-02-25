using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.HttpTrigger.Services;

namespace Azure.HttpTrigger.Functions
{
    public class GetUsers
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetUsers> _log;

        public GetUsers(IUserService userService)
        {
            _userService = userService;
        }


        [FunctionName("GetUsers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                return new OkObjectResult(users);
            }
            catch(Exception ex)
            {
                throw new Exception($"Exception {ex.Message} occured at {ex.StackTrace}");
            }
        }
    }
}
