using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;
using YLunch.Domain.Repositories;

namespace YLunch.Infrastructure.Database.Repositories
{
    public class RestaurantProductRepository : IRestaurantProductRepository
    {
        private readonly ApplicationDbContext _context;

        public RestaurantProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(RestaurantProduct restaurantProduct)
        {
            await _context.RestaurantProducts.AddAsync(restaurantProduct);
            await _context.SaveChangesAsync();
        }

        public async Task Update()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<RestaurantProduct>> GetAll(RestaurantProductsFilter restaurantProductsFilter)
        {
            return await _context.RestaurantProducts
                .Where(x => restaurantProductsFilter.RestaurantId == null ||
                            x.RestaurantId == restaurantProductsFilter.RestaurantId)
                .Where(x => restaurantProductsFilter.IsActive == null ||
                            x.IsActive == restaurantProductsFilter.IsActive)
                .Where(x => restaurantProductsFilter.QuantityMin == null ||
                            x.Quantity >= restaurantProductsFilter.QuantityMin)
                .Where(x => restaurantProductsFilter.QuantityMax == null ||
                            x.Quantity <= restaurantProductsFilter.QuantityMax)
                .ToListAsync();
        }

        public async Task<RestaurantProduct> GetById(string restaurantProductId)
        {
            return await _context.RestaurantProducts.FindAsync(restaurantProductId);
        }

        public async Task Delete(string restaurantProductId)
        {
            var restaurantProduct =
                await _context.RestaurantProducts.FirstOrDefaultAsync(x => x.Id.Equals(restaurantProductId));
            _context.RestaurantProducts.Remove(restaurantProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<RestaurantProduct>> GetAllEligibleForCustomerByRestaurantIdByProductIds(
            ICollection<string> orderedRestaurantProductsIds,
            string restaurantId)
        {
            var restaurantProducts =
                await GetAll(new RestaurantProductsFilter { RestaurantId = restaurantId, IsActive = true });

            return orderedRestaurantProductsIds
                .Where(id => restaurantProducts.Any(rp => rp.Id.Equals(id)))
                .Select(id => _context.RestaurantProducts.Find(id))
                .ToList();
        }
    }
}
