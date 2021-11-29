using System.Collections.Generic;
using System.Threading.Tasks;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;

namespace YLunch.Domain.Repositories
{
    public interface IRestaurantRepository
    {
        Task Create(Restaurant restaurant);
        Task Update();
        Task<Restaurant> GetById(string id);
        Task<Restaurant> GetByUserId(string id);
        Task<ICollection<RestaurantCategory>> GetAllRestaurantCategories();
        Task<ICollection<Restaurant>> GetAllForCustomer(RestaurantsFilter restaurantsFilter);
        Task<ICollection<Restaurant>> GetAll(RestaurantsFilter restaurantsFilter);
        Task<Restaurant> GetByIdIncludingProducts(string id);
        Task DeleteById(string id);
        Task<Restaurant> GetByIdForCustomer(string id);
    }
}
