using Azure.CRUD.Models;
using Azure.CRUD.Resources;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.CRUD.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository() { }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using(var connection = Helper.DbConnection)
            {
                var users = await connection.QueryAsync<User>("Select Id, Name from Users");
                return users;
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using(var connection = Helper.DbConnection)
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>("Select Id, Name from Users Where Id=@id", new {id});
                return user;
            }
        }
    }
}
