using Azure.ServiceBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.ServiceBus.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UpdateUserAsync(User user);
    }
}
