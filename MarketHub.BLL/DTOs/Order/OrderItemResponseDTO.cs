using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Order
{
    public class OrderItemResponseDTO
    {
        //public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quentity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
