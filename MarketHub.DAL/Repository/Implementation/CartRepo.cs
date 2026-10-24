using System.Net;

namespace MarketHub.DAL.Repository.Implementation
{
    public class CartRepo : ICartRepo
    {
        private readonly AppDbContext _context;
        public CartRepo(AppDbContext context)
        {
            _context = context;
        }

        public Cart GetCartWithItemsByUserId(string userId) =>
            _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefault(c => c.UserId == userId);

        public bool CreateUserCart(Cart cart)
        {
            _context.Carts.Add(cart);
            if (_context.SaveChanges() > 0)
                return true;

            return false;
        }

        public bool AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);

            if (_context.SaveChanges() > 0)
                return true;

            return false;
        }
        //all items in one cart 
        public List<CartItem> GetItemsByCartIdWithProducts(int cartId)
            => _context.CartItems.Include(ci => ci.Product).Where(ci => ci.CartId == cartId).AsNoTracking().ToList();

        public bool DeletaCart(Cart cart)
        {
            _context.Carts.Remove(cart);

            if (_context.SaveChanges() > 0)
                return true;

            return false;
        }
        public bool SaveChanges()
            => _context.SaveChanges() > 0;

        // get one item by its id 
        public CartItem? GetItemByIdWithProduct(int cartItemId)
            => _context.CartItems.Include(ci => ci.Product).FirstOrDefault(ci => ci.Id == cartItemId);
        
        public bool UpdateItem(CartItem cartItem)
        {
           _context.CartItems.Update(cartItem);
            return _context.SaveChanges() > 0;
        }

        public CartItem? GetItemById(int cartItemId)
          => _context.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);

        public bool RemoveItem(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);

            return _context.SaveChanges() > 0 ? true : false;
        }


    }
}
