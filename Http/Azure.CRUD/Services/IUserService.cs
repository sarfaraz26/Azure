using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.CRUD.Services
{
    public interface IUserService
    {
        Task<object> GetUsersAsync(string id);
        Task<string> SendAddUserObjectToQueueAsync(string requestBody);
        Task<string> SendDeleteUserObjectToQueueAsync(int id);
        Task<string> SendUpdateUserObjectToQueueAsync(int id, string requestBody);
    }
}
