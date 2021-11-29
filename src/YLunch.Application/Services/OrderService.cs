using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YLunch.Application.Exceptions;
using YLunch.Domain.DTO.OrderModels;
using YLunch.Domain.DTO.OrderModels.OrderStatusModels;
using YLunch.Domain.ModelsAggregate.CustomerAggregate;
using YLunch.Domain.ModelsAggregate.OrderAggregate;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;
using YLunch.Domain.Repositories;
using YLunch.Domain.Services;
using YLunch.DomainShared.RestaurantAggregate.Enums;

namespace YLunch.Application.Services
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
                await _restaurantProductRepository.GetAllEligibleForCustomerByRestaurantIdByProductIds(
                    orderCreationDto.ProductsId, orderCreationDto.RestaurantId);
            if (restaurantProducts.Count != orderCreationDto.ProductsId.Count)
                throw new NotFoundException("Not found all products");

            var orderId = Guid.NewGuid().ToString();
            var customerProducts = restaurantProducts
                .Select(x => CustomerProduct.Create(x, orderId)).ToList();

            var order = Order.Create(orderId, orderCreationDto, customer, customerProducts);
            await _orderRepository.Create(order);
            return new OrderReadDto(order);
        }

        public async Task<ICollection<OrderReadDto>> AddStatusToMultipleOrders(
            AddOrderStatusToMultipleOrdersDto addOrderStatusToMultipleOrdersDto)
        {
            ICollection<Order> orders =
                await _orderRepository.GetAllByIds(addOrderStatusToMultipleOrdersDto.OrdersId);

            foreach (var o in orders)
            {
                // check order status is eligible and next
                if (addOrderStatusToMultipleOrdersDto.State != OrderState.Canceled &&
                    addOrderStatusToMultipleOrdersDto.State != OrderState.Rejected &&
                    addOrderStatusToMultipleOrdersDto.State != OrderState.Other &&
                    o.CurrentOrderStatus.State != addOrderStatusToMultipleOrdersDto.State - 1
                )
                {
                    throw new BadNewOrderStateException(
                        $"order: {o.Id} is not in the previous state the new requested state");
                }

                var newStatus = OrderStatus.Create(o.Id, addOrderStatusToMultipleOrdersDto.State);
                o.OrderStatuses.Add(newStatus);
            }

            await _orderRepository.Update();
            return orders.Select(x => new OrderReadDto(x)).ToList();
        }

        public async Task<ICollection<OrderReadDto>> GetNewOrdersByRestaurantId(string restaurantId)
        {
            var orders = await _orderRepository.GetNewOrdersByRestaurantId(restaurantId);
            return orders
                .Select(x => new OrderReadDto(x))
                .ToList();
        }

        public async Task<ICollection<OrderReadDto>> GetAll(OrdersFilter ordersFilter)
        {
            var orders = await _orderRepository.GetAll(ordersFilter);
            return orders
                .Select(x => new OrderReadDto(x))
                .ToList();
        }
    }
}
