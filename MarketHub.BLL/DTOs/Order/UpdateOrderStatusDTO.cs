
using System.ComponentModel.DataAnnotations;

namespace MarketHub.BLL.DTOs.Order
{
    public class UpdateOrderStatusDTO
    {
        [Required(ErrorMessage = "OrderStatus Is Required")]
        [MinLength(7, ErrorMessage = "OrderStatus Must be at least 7 Characters")]
        public string? OrderStatus { get; set; }

    }
}
