
using MarketHub.DAL.Entities;

namespace MarketHub.DAL.Repository.Abstraction
{
    public interface ICartRepo
    {
        public bool AddCartItem(CartItem? cartItem);
        public Cart GetCartWithItemsByUserId(string? userId);
        public bool CreateUserCart(Cart? cart);
        List<CartItem> GetItemsByCartIdWithProducts(int cartId);
        public bool DeletaCart(Cart cart);
        public CartItem? GetItemById(int cartItemId);
        public bool UpdateItem(CartItem cartItem);
        public CartItem? GetItemByIdWithProduct(int cartItemId);
        public bool RemoveItem(CartItem cartItem);
        public bool SaveChanges();

    }
}
