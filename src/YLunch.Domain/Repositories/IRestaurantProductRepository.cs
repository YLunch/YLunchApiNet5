using System.Collections.Generic;
using System.Threading.Tasks;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;

namespace YLunch.Domain.Repositories
{
    public interface IRestaurantProductRepository
    {
        Task Create(RestaurantProduct restaurantProduct);
        Task Update();
        Task<ICollection<RestaurantProduct>> GetAll(RestaurantProductsFilter restaurantProductsFilter);
        Task Delete(string restaurantProductId);
        Task<ICollection<RestaurantProduct>> GetAllEligibleForCustomerByRestaurantIdByProductIds(ICollection<string> orderedRestaurantProductsIds, string restaurantId);
        Task<RestaurantProduct> GetById(string restaurantProductId);
    }
}
