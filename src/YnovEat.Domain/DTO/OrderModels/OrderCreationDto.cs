using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YnovEat.Domain.DTO.OrderModels
{
    public class OrderCreationDto
    {
        [Required] public ICollection<string> ProductsId { get; set; }
        public string Comment { get; set; }
    }
}
