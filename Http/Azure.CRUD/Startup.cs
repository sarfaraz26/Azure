using Azure.CRUD;
using Azure.CRUD.Repositories;
using Azure.CRUD.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Azure.CRUD
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
