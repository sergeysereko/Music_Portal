using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.BLL.DTO;

namespace MusicPortal.BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDTO userDto);
        Task UpdateUser(UserDTO teamDto);
        Task DeleteUser(int id);
        Task<UserDTO> GetUser(int id);
        Task<IEnumerable<UserDTO>> GetUsers();
    }
}
