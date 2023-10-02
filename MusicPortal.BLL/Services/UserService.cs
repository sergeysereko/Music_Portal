using MusicPortal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Models;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace MusicPortal.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateUser(UserDTO userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                Salt = userDto.Salt,
                Access = userDto.Access
            };
            await Database.Users.Create(user);
            await Database.Save();
        }

        public async Task UpdateUser(UserDTO userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                Salt = userDto.Salt,
                Access = userDto.Access
            };
            Database.Users.Update(user);
            await Database.Save();
        }

        public async Task DeleteUser(int id)
        {
            await Database.Users.Delete(id);
            await Database.Save();
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var user = await Database.Users.Get(id);
            if (user == null)
            {
                throw new ValidationException("Wrong user!");
            }
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                Access = user.Access
            };
        }

        public async Task<UserDTO> GetUser(string login)
        {
            var user = await Database.Users.Get(login);
            if (user == null)
            {
                throw new ValidationException("Wrong user!");
            }
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                Access = user.Access
            };
        }


        
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetAll());
        }


    }

}


