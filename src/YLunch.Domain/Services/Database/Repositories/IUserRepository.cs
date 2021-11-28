using System.Collections.Generic;
using System.Threading.Tasks;
using YLunch.Domain.ModelsAggregate.UserAggregate;

namespace YLunch.Domain.Services.Database.Repositories
{
    public interface IUserRepository
    {
        Task Register(User user, string password, string role);
        Task<User> GetFullUser(string username);
        Task<ICollection<User>> GetFullUsers();
        Task<User> GetCustomerById(string id);
        Task DeleteByUsername(string username);
    }
}
