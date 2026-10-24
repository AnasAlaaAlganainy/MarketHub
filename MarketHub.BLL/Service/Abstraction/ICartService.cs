using MarketHub.BLL.DTOs.Cart;
using MarketHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.Service.Abstraction
{
    public interface ICartService
    {
        public GenResponse GetCart(string? userId);
        public GenResponse AddCartItem(AddCartItemDTO addCartItemDTO, string? userId);
        public GenResponse ClearCart(string? userId);
        public GenResponse UpdateCartItem(int cartItemId, UpdateCartItemDTO? updateCartItemDTO, string userId);
        public GenResponse RemoveCartItem(int cartItemId, string ?userId);

    }
}
