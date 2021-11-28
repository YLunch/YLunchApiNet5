using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YLunch.Domain.ModelsAggregate.OrderAggregate;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;
using YLunch.Domain.Services.OrderServices;
using YLunch.DomainShared.RestaurantAggregate.Enums;

namespace YLunch.Infrastructure.Database.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Order>> GetAllByRestaurantId(string restaurantId)
        {
            return await _context.Orders
                .Include(x => x.OrderStatuses)
                .Include(x => x.CustomerProducts)
                .Where(o => o.RestaurantId.Equals(restaurantId))
                .Where(o => o.CreationDateTime > DateTime.Today)
                .OrderBy(o => o.ReservedForDateTime)
                .ToListAsync();
        }

        public async Task<ICollection<Order>> GetAll(OrdersFilter filter)
        {
            return await _context.Orders
                .Include(x => x.OrderStatuses)
                .Include(x => x.CustomerProducts)
                .Where(o => filter.RestaurantId == null || o.RestaurantId == filter.RestaurantId)
                .Where(x => filter.Status == null || x.OrderStatuses.All(y => y.State == filter.Status))
                .Where(o => filter.AfterDateTime == null || o.CreationDateTime > filter.AfterDateTime)
                .OrderBy(o => o.ReservedForDateTime)
                .ToListAsync();
        }

        public async Task<Order> GetById(string id)
        {
            return await _context.Orders
                .Include(x => x.OrderStatuses)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task Update()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Order>> GetAllByIds(ICollection<string> ordersIds)
        {
            return await _context.Orders
                .Include(x => x.OrderStatuses)
                .Where(o => ordersIds.Contains(o.Id))
                .OrderBy(o => o.ReservedForDateTime)
                .ToListAsync();
        }

        public async Task<ICollection<Order>> GetNewOrdersByRestaurantId(string restaurantId)
        {
            return await _context.Orders
                .Include(x => x.OrderStatuses)
                .Where(x => x.RestaurantId.Equals(restaurantId))
                .Where(x => x.OrderStatuses.All(y => y.State == OrderState.Idling))
                .OrderBy(o => o.ReservedForDateTime)
                .ToListAsync();
        }
    }
}
