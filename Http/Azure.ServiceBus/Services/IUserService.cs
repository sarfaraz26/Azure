using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.ServiceBus.Services
{
    public interface IUserService
    {
        Task<bool> AddUserToDBFromQueue(dynamic param);
        Task<bool> DeleteUserFromDbReceivedFromQueue(int id);
        Task<bool> UpdateUserToDBFromQueue(dynamic param);
    }
}
