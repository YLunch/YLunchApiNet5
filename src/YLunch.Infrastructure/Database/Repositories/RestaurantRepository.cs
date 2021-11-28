using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using YLunch.Application.Exceptions;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;
using YLunch.Domain.Services.Database.Repositories;

namespace YLunch.Infrastructure.Database.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ApplicationDbContext _context;

        public RestaurantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private IIncludableQueryable<Restaurant, ICollection<OpeningTime>> QueryEnrichedRestaurants =>
            _context.Restaurants
                .Include(x => x.ClosingDates)
                .Include(x => x.Categories)
                .Include(x => x.WeekOpeningTimes.OrderBy(y => y.DayOfWeek))
                .ThenInclude(x => x.OpeningTimes);

        public async Task<Restaurant> GetById(string id) =>
            await QueryEnrichedRestaurants
                .Include(x => x.RestaurantProducts)
                .Include(x => x.RestaurantUsers)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<Restaurant> GetByIdForCustomer(string id) =>
            await QueryEnrichedRestaurants
                .Where(x => x.IsPublished)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<Restaurant> GetByIdIncludingProducts(string id) =>
            await QueryEnrichedRestaurants
                .Include(x => x.RestaurantProducts)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<Restaurant> GetByUserId(string id) =>
            await QueryEnrichedRestaurants
                .FirstOrDefaultAsync(x => x.RestaurantUsers
                    .Any(y => y.UserId.Equals(id))
                );

        public async Task<ICollection<RestaurantCategory>> GetAllRestaurantCategories()
        {
            return await _context.RestaurantCategories.ToListAsync();
        }

        public async Task<ICollection<Restaurant>> GetAllForCustomer(RestaurantsFilter restaurantsFilter)
        {
            return await QueryEnrichedRestaurants
                .Where(x => x.IsPublished)
                .ToListAsync();
        }

        public async Task<ICollection<Restaurant>> GetAll(RestaurantsFilter restaurantsFilter)
        {
            return await QueryEnrichedRestaurants
                .Include(x => x.RestaurantUsers)
                .Where(x => restaurantsFilter.IsPublished == null || x.IsPublished == restaurantsFilter.IsPublished)
                .ToListAsync();
        }

        public async Task Create(Restaurant restaurant)
        {
            var owner = await _context.RestaurantUsers.FirstAsync(x => x.User.Id.Equals(restaurant.OwnerId));
            owner.RestaurantId = restaurant.Id;
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task Update()
        {
            var uselessCategories =
                await _context.RestaurantCategories
                    .Where(x => x.Restaurants.Count == 0).ToListAsync();
            _context.RemoveRange(uselessCategories);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(string id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                throw new NotFoundException($"Restaurant {id} not found");
            }
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
