using Azure.HttpTrigger.Models;
using Azure.HttpTrigger.Resources;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.HttpTrigger.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUserByIdAsync(int id)
        {
            using (var connection = Helper.DbConnection)
            {
                connection.Open();
                var user = await connection.QuerySingleOrDefaultAsync<User>("Select ID, Name, IsManager from Users Where ID=@Id", new {Id = id});
                return user;
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using(var connection = Helper.DbConnection)
            {
                connection.Open();
                var users = await connection.QueryAsync<User>("Select ID, Name, IsManager from Users");
                return users;
            }
        }
    }
}
