using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Azure.TimerTrigger.ViewModels;
using Dapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Azure.TimerTrigger
{
    public class SendWarning
    {
        [FunctionName("SendWarning")]
        public async Task Run([TimerTrigger("00 22 * * *")]TimerInfo myTimer, ILogger log)
        {
            try
            {
                using(IDbConnection connection = Helper.DbConnection)
                {
                    int warningDays = Helper.GetWarningDays;
                    
                    var parameters = new
                    {
                        WarningDays = warningDays
                    };

                    var warnedUsers = await connection.QueryAsync<UserViewModel>("select Name, Email, LastLoginDtTm from users WHERE DATEDIFF(DAY, LastLoginDtTm ,GETUTCDATE()) >= @WarningDays", parameters);

                    log.LogInformation(JsonConvert.SerializeObject(warnedUsers, Formatting.Indented));
                }
            }
            catch(Exception ex)
            {
                log.LogError($"Exception occured : {ex.Message} at {ex.StackTrace}");
            }
        }
    }
}
