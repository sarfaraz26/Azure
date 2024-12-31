using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Data;
using HttpTrigger.Models;
using System.Collections.Generic;

namespace HttpTrigger
{
    public class GetEmployee
    {
        [FunctionName("GetEmployee")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "getemployee")] HttpRequest req,
            ILogger log)
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("DBConnectionString").ToString();
                string id = req.Query["id"];
                List<Employee> employees = new();

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_GetEmployee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = String.IsNullOrEmpty(id) ? null : Convert.ToInt64(id);
                        connection.Open();

                        using(var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Employee employee = new Employee()
                                {
                                    Id = Convert.ToInt32(reader["ID"].ToString()),
                                    Name = reader["Name"].ToString(),
                                    Age = Convert.ToInt32(reader["ID"].ToString()),
                                    City = reader["City"].ToString()
                                };

                                employees.Add(employee);
                            }
                        }
                    }
                }

                return new OkObjectResult(employees);
            }
            catch(Exception ex)
            {
                log.LogError($"Exception occured {ex.Message} at {ex.StackTrace}");
                throw;
            }
        }
    }
}
