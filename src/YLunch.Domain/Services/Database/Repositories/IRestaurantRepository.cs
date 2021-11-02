using System.Collections.Generic;
using System.Threading.Tasks;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate;

namespace YLunch.Domain.Services.Database.Repositories
{
    public interface IRestaurantRepository
    {
        Task Create(Restaurant restaurant);
        Task Update();
        Task<Restaurant> GetById(string id);
        Task<Restaurant> GetByUserId(string id);
        Task<ICollection<RestaurantCategory>> GetAllRestaurantCategories();
        Task<ICollection<Restaurant>> GetAllForCustomer();
        Task<ICollection<Restaurant>> GetAll();
        Task<Restaurant> GetByIdIncludingProducts(string id);
    }
}
