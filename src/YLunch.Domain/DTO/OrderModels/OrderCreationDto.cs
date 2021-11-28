using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YLunch.Domain.DTO.OrderModels
{
    public class OrderCreationDto
    {
        [Required] public ICollection<string> ProductsId { get; set; }
        public string CustomerComment { get; set; }
        public string RestaurantComment { get; set; }
        [Required] public DateTime ReservedForDateTime { get; set; }
        [Required] public string RestaurantId { get; set; }
    }
}
