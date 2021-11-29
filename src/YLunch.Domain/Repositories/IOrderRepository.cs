using System.Collections.Generic;
using System.Threading.Tasks;
using YLunch.Domain.ModelsAggregate.OrderAggregate;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;

namespace YLunch.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task Create(Order order);
        Task<ICollection<Order>> GetAllByRestaurantId(string restaurantId);
        Task<Order> GetById(string id);
        Task Update();
        Task<ICollection<Order>> GetAllByIds(ICollection<string> ordersIds);
        Task<ICollection<Order>> GetNewOrdersByRestaurantId(string restaurantId);
        Task<ICollection<Order>> GetAll(OrdersFilter filter);
    }
}
