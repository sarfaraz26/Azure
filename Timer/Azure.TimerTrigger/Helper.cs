using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Azure.TimerTrigger
{
    public static class Helper
    {
        public static IDbConnection DbConnection 
        {
            get
            {
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? string.Empty;
                return new SqlConnection(connectionString);
            }
        }

        public static int GetExpirationDays
        {
            get
            {
                return GetDaysFromConfig("ExpirationDays");
            }
        }

        public static int GetWarningDays
        {
            get
            {
                return GetDaysFromConfig("WarningDays");
            }
        }

        #region Methods
        private static int GetDaysFromConfig(string param)
        {
            var configDays = Environment.GetEnvironmentVariable(param);
            int days = !string.IsNullOrEmpty(configDays) 
                        ? Convert.ToInt32(configDays) 
                        : (param == "ExpirationDays") ? 7 : 3;
            return days;
        }
        #endregion
    }
}
