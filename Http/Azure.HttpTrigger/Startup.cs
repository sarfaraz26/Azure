using Azure.HttpTrigger;
using Azure.HttpTrigger.Models;
using Azure.HttpTrigger.Repositories;
using Azure.HttpTrigger.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Azure.HttpTrigger
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
