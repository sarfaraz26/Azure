using Azure.ServiceBus.Models;
using Azure.ServiceBus.Resources;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.ServiceBus.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<bool> AddUserAsync(User user)
        {
            using (var connection = Helper.DbConnection)
            {
                var parameters = new
                {
                    name = user.Name,
                    isAdmin = user.IsAdmin
                };

                var affectedRows = await connection.ExecuteAsync("INSERT INTO Users (Name, IsAdmin) VALUES(@name, @isAdmin)", parameters);
                return affectedRows > 0;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            using (var connection = Helper.DbConnection)
            {
                var affectedRows = await connection.ExecuteAsync("DELETE FROM Users WHERE Id=@id", new { id });
                return affectedRows > 0;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            using (var connection = Helper.DbConnection)
            {
                var parameters = new
                {
                    id = user.Id,
                    name = user.Name,
                    isAdmin = user.IsAdmin
                };
                var affectedRows = await connection.ExecuteAsync("UPDATE Users SET Name=@name, IsAdmin=@isAdmin WHERE Id=@id", parameters);
                return affectedRows > 0;
            }
        }
    }
}
