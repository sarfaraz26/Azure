using Azure.ServiceBus.Models;
using Azure.ServiceBus.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.ServiceBus.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }
        
        public async Task<bool> AddUserToDBFromQueue(dynamic param)
        {
            User user = new() { Name = Convert.ToString(param.name), IsAdmin = Convert.ToBoolean(param.isAdmin) };
            var response = await _userRepository.AddUserAsync(user);
            return response;
        }

        public async Task<bool> DeleteUserFromDbReceivedFromQueue(int id)
        {
            var response = await _userRepository.DeleteUserAsync(id);
            return response;
        }

        public async Task<bool> UpdateUserToDBFromQueue(dynamic param)
        {
            User user = new() { Id = param.id, Name = param.name, IsAdmin = param.isAdmin };
            var response = await _userRepository.UpdateUserAsync(user);
            return response;
        }
    }
}
