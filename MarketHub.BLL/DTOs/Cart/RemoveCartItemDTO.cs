using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Cart
{
    public class RemoveCartItemDTO
    {
        [Required(ErrorMessage = "Product Id Required")]
        [Range(minimum: 1, maximum: int.MaxValue,
            ErrorMessage = "Product Id Must Be At Least 1")]
        public int ProductId { get; set; }
    }
}
