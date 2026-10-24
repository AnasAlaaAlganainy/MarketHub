using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Order
{
    public class GetOrderByIdDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        //public string UserId { get; set; }
        public string? ShippingAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
            = MarketHub.DAL.Enums.OrderStatus.Pending.ToString();
        public List<OrderItemResponseDTO> OrderItems { get; set; } = new List<OrderItemResponseDTO>();

    }
}
