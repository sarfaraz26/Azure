using Azure.CRUD.DTOs;
using Azure.CRUD.Models;
using Azure.CRUD.Repositories;
using Azure.CRUD.Resources;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.CRUD.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Get from DB
        public async Task<object> GetUsersAsync(string id)
        {
            if (string.IsNullOrEmpty(id) == false)
            {
                if (Convert.ToInt32(id) < 1)
                {
                    throw new ArgumentException($"Invalid id:{id} passed");
                }
                else
                {
                    var userFromDb = await _userRepository.GetUserByIdAsync(Convert.ToInt32(id));
                    var user = new UserViewModel
                    {
                        Id = userFromDb.Id,
                        Name = userFromDb.Name
                    };
                    return user;
                }
            }
            else
            {
                var usersFromDb = await _userRepository.GetUsersAsync();
                var users = usersFromDb.Select(user =>
                                new UserViewModel
                                {
                                    Id = user.Id,
                                    Name = user.Name
                                }).ToList();
                return users;
            }
        }
        #endregion


        #region Send To Queues
        public async Task<string> SendAddUserObjectToQueueAsync(string requestBody)
        {
            AddUserDTO addUserDTO = JsonConvert.DeserializeObject<AddUserDTO>(requestBody) ?? new AddUserDTO();

            var opts = new QueueDTO
            {
                PayLoad = addUserDTO,
                Operation = "Add"
            };

            string serialzedDto = JsonConvert.SerializeObject(opts);
            string response = await Helper.SendMessageToQueue(serialzedDto);
            return response;
        }

        public async Task<string> SendDeleteUserObjectToQueueAsync(int id)
        {
            DeleteUserDTO deleteUserDTO = new() { Id = id };

            var opts = new QueueDTO
            {
                PayLoad = deleteUserDTO,
                Operation = "Delete"
            };

            string serialzedDto = JsonConvert.SerializeObject(opts);
            string response = await Helper.SendMessageToQueue(serialzedDto);
            return response;
        }

        public async Task<string> SendUpdateUserObjectToQueueAsync(int id, string requestBody)
        {
            UpdateUserDTO updateUserDTO = JsonConvert.DeserializeObject<UpdateUserDTO>(requestBody) ?? new UpdateUserDTO();
            updateUserDTO.Id = id;

            var opts = new QueueDTO
            {
                PayLoad = updateUserDTO,
                Operation = "Update"
            };

            string serialzedDto = JsonConvert.SerializeObject(opts);
            string response = await Helper.SendMessageToQueue(serialzedDto);
            return response;
        }
        #endregion

    }
}
