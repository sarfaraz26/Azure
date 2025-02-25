using Azure.HttpTrigger.Repositories;
using Azure.HttpTrigger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.HttpTrigger.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<UserViewModel> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            var response = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name
            };

            return response;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();

            var response = users.Select(user =>
            {
                return new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name
                };
            }).ToList();

            return response;
        }
    }
}
