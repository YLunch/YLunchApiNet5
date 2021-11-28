using System;
using YLunch.DomainShared.RestaurantAggregate.Enums;

namespace YLunch.Domain.ModelsAggregate.OrderAggregate
{
    public class OrdersFilter
    {
        public DateTime? AfterDateTime { get; init; }
        public OrderState? Status { get; init; }
        public string RestaurantId { get; init; }
    }
}
