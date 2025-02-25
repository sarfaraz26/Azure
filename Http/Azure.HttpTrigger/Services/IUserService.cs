using Azure.HttpTrigger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.HttpTrigger.Services
{
    public interface IUserService
    {
        Task<UserViewModel> GetUserByIdAsync(int id);
        Task<IEnumerable<UserViewModel>> GetUsersAsync();
    }
}
