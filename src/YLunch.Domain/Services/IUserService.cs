using System.Collections.Generic;
using System.Threading.Tasks;
using YLunch.Domain.DTO.UserModels;

namespace YLunch.Domain.Services
{
    public interface IUserService
    {
        Task<ICollection<UserReadDto>> GetAllUsers();
        Task<UserAsCustomerDetailsReadDto> GetCustomerById(string id);
        Task DeleteUserByUsername(string username);
    }
}
