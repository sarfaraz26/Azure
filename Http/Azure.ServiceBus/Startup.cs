using Azure.ServiceBus;
using Azure.ServiceBus.Repositories;
using Azure.ServiceBus.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Azure.ServiceBus
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
