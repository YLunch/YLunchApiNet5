namespace YLunch.Domain.ModelsAggregate.RestaurantAggregate
{
    public class RestaurantProductsFilter
    {
        public bool? IsActive { get; init; }
        public int? QuantityMin { get; init; }
        public int? QuantityMax { get; init; }
        public string RestaurantId { get; init; }
    }
}
