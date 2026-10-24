using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Cart
{
    public class GetCartDTO
    {
        public int CartId { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuentity { get; set; }
        public List<GetCartItemDTO> CartItems { get; set; } = new List<GetCartItemDTO>();

    }
}
