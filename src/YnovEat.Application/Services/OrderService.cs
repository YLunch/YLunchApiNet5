using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YnovEat.Application.Exceptions;
using YnovEat.Domain.DTO.OrderModels;
using YnovEat.Domain.DTO.OrderModels.OrderStatusModels;
using YnovEat.Domain.ModelsAggregate.CustomerAggregate;
using YnovEat.Domain.ModelsAggregate.RestaurantAggregate;
using YnovEat.Domain.Services.Database.Repositories;
using YnovEat.Domain.Services.OrderServices;

namespace YnovEat.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRestaurantProductRepository _restaurantProductRepository;

        public OrderService(IOrderRepository orderRepository, IRestaurantProductRepository restaurantProductRepository)
        {
            _orderRepository = orderRepository;
            _restaurantProductRepository = restaurantProductRepository;
        }

        public async Task<OrderReadDto> Create(OrderCreationDto orderCreationDto, Customer customer)
        {
            var restaurantProducts =
                    _restaurantProductRepository.GetListByIds(orderCreationDto.ProductsId);
            if (restaurantProducts.Count != orderCreationDto.ProductsId.Count)
                throw new NotFoundException("Not found all products");

                var orderId = Guid.NewGuid().ToString();
            var customerProducts = restaurantProducts
                .Select(x => CustomerProduct.Create(x, orderId)).ToList();

            var order = Order.Create(orderId, orderCreationDto, customer, customerProducts);
            await _orderRepository.Create(order);
            return new OrderReadDto(order);
        }

        public async Task<OrderReadDto> AddStatus(OrderStatusCreationDto orderStatusCreationDto)
        {
            var order = await _orderRepository.GetById(orderStatusCreationDto.OrderId);
            order.OrderStatuses.Add(OrderStatus.Create(orderStatusCreationDto));
            await _orderRepository.Update();
            return new OrderReadDto(order);
        }

        public async Task<ICollection<OrderReadDto>> AddStatusToMultipleOrders(
            AddOrderStatusToMultipleOrdersDto addOrderStatusToMultipleOrdersDto)
        {
            ICollection<Order> orders =
                await _orderRepository.GetAllById(addOrderStatusToMultipleOrdersDto.OrdersId);

            foreach (var o in orders)
            {
                if (o.CurrentOrderStatus.State != addOrderStatusToMultipleOrdersDto.State - 1)
                    throw new BadNewOrderStateException($"order: {o.Id} is not in the previous state the new requested state");
                var newStatus = OrderStatus.Create(o.Id, addOrderStatusToMultipleOrdersDto.State);
                o.OrderStatuses.Add(newStatus);
            }

            await _orderRepository.Update();
            return orders.Select(x => new OrderReadDto(x)).ToList();
        }

        public async Task<ICollection<OrderReadDto>> GetNewOrdersByRestaurantId(string restaurantId)
        {
            var orders = await _orderRepository.GetNewOrdersByRestaurantId(restaurantId);
            return orders.Select(x => new OrderReadDto(x)).ToList();
        }
    }
}
