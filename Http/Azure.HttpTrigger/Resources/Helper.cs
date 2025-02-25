using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.HttpTrigger.Resources
{
    public class Helper
    {
        public static IDbConnection DbConnection
        {
            get
            {
                var connectionString = Environment.GetEnvironmentVariable("CS") ?? string.Empty;
                return new SqlConnection(connectionString);
            }
        }
    }
}
