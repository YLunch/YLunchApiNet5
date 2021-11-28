using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YLunch.Domain.DTO.OrderModels;
using YLunch.Domain.DTO.OrderModels.OrderStatusModels;
using YLunch.Domain.ModelsAggregate.CustomerAggregate;
using YLunch.Domain.ModelsAggregate.OrderAggregate;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;

namespace YLunch.Domain.Services.OrderServices
{
    public interface IOrderService
    {
        Task<OrderReadDto> Create(OrderCreationDto orderCreationDto, Customer customer);

        Task<ICollection<OrderReadDto>> AddStatusToMultipleOrders(
            AddOrderStatusToMultipleOrdersDto addOrderStatusToMultipleOrdersDto);

        Task<ICollection<OrderReadDto>> GetNewOrdersByRestaurantId(string restaurantId);
        Task<ICollection<OrderReadDto>> GetAll(OrdersFilter ordersFilter);
    }
}
