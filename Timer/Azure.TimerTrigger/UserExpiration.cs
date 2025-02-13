using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Azure.TimerTrigger
{
    public class UserExpiration
    {
        [FunctionName("UserExpiration")]
        public async Task Run([TimerTrigger("30 22 * * *")]TimerInfo myTimer, ILogger log)
        {
            try
            {
                using(IDbConnection connection = Helper.DbConnection)
                {
                    var parameters = new
                    {
                        ExpirationDay = Helper.GetExpirationDays
                    };

                    int rows = await connection.ExecuteAsync("usp_MarkUserExpire", parameters, commandType: CommandType.StoredProcedure);
                    log.LogInformation($"Affected rows : {rows}");
                }
            }
            catch(Exception ex)
            {
                log.LogError($"Exception occured : {ex.Message} at {ex.StackTrace}");
            }
        }
    }
}
