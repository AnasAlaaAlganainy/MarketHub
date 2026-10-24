using MarketHub.BLL.DTOs.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Runtime.Intrinsics.Arm;
using System.Security.AccessControl;
using System.Security.Claims;

namespace MarketHub.BLL.Service.Implementation
{
    public class CartService: ICartService
    {
        //DI
        private readonly ICartRepo _cartRepo;
        private readonly IProductRepo _productRepo;
        public CartService(ICartRepo cartRepo, IProductRepo productRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
        }

        public GenResponse GetCart(string? userId)
        {
            try
            {
                Cart? cart = _cartRepo.GetCartWithItemsByUserId(userId);

                //if user dont have cart, create empty one to him

                if(cart == null)
                {
                    cart = new Cart();
                    cart.UserId = userId;
                    if (_cartRepo.CreateUserCart(cart) == false)
                        return CreateResponse(false, "Internal Server Error", null);
                }


                //mapping 
                GetCartDTO cartDTO = new GetCartDTO();

                cartDTO.CartId = cart.Id;
                cart.CartItems = _cartRepo.GetItemsByCartIdWithProducts(cart.Id);
                cartDTO.TotalQuentity = 0;//cart.CartItems.Count();

                foreach (var item in cart.CartItems)
                {
                    GetCartItemDTO cartItemDTO = new GetCartItemDTO();

                    cartItemDTO.CartItemId = item.Id;
                    cartItemDTO.ProductId = item.ProductId;
                    cartItemDTO.ProductName = item.Product.Name;
                    cartItemDTO.ProductPrice = item.Product.Price;
                    cartItemDTO.Quentity = item.Quentity;

                    cartDTO.CartItems.Add(cartItemDTO);
                    cartDTO.TotalQuentity += item.Quentity; 
                    cartDTO.TotalPrice += cartItemDTO.ProductPrice * cartItemDTO.Quentity; ;
                }

                return CreateResponse(true, "This Is Your Cart", cartDTO);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, "Internal Server Error ", null);
            }
        }


        public GenResponse AddCartItem(AddCartItemDTO addCartItemDTO, string? userId)
        {
            //GenResponse response = new GenResponse();
            try
            {
                //find if user have cart or not
                Cart cart = _cartRepo.GetCartWithItemsByUserId(userId);
                
                if (cart == null)//user don't have cart 
                {
                    cart = new Cart();
                    cart.UserId = userId;
                    cart.CreatedAt = DateTime.Now;

                    if (_cartRepo.CreateUserCart(cart) == false)
                        return CreateResponse(false, "Error With User Cart", addCartItemDTO);
                }

                //if productid exist in db
                Product product = _productRepo.GetById(addCartItemDTO.ProductId);

                if (product == null)
                    return CreateResponse(false, "Product Id not Exist", addCartItemDTO);
                
                //if product stock quentity empty
                if(product.StockQuantity < 1)
                    return CreateResponse(false, "Product Stock Quentity Empty", null);

                //if product already added
                foreach (var item in cart.CartItems)
                {
                    if(item.ProductId == addCartItemDTO.ProductId)
                        return CreateResponse(true, "This Product Already Added In The Cart", addCartItemDTO);
                }
                //List<CartItem> cartItems = _cartRepo.GetCartItems();

                //Create CartItem in This Cart
                CartItem cartItem = new CartItem();
                cartItem.CartId = cart.Id;
                cartItem.Quentity = 1;
                cartItem.ProductId = product.Id;
                
                //Error
                if(_cartRepo.AddCartItem(cartItem) == false)
                    return CreateResponse(false, "Internal Server Error ", addCartItemDTO);

                //prepare GetCartItemDTO Response
                GetCartItemDTO getCartItemDTO = new GetCartItemDTO();
                
                getCartItemDTO.CartItemId = cartItem.Id;
                getCartItemDTO.Quentity = cartItem.Quentity;
                getCartItemDTO.ProductPrice = product.Price;
                getCartItemDTO.ProductName = product.Name;
                getCartItemDTO.ProductId = product.Id;

                //return response
                return CreateResponse(true, "Product Added Successfully", getCartItemDTO);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, "Internal Server Error ", addCartItemDTO);
            }

        }


        public GenResponse ClearCart(string? userId)
        {
            Cart cart = _cartRepo.GetCartWithItemsByUserId(userId);
            
            // user don't have cart 
            if (cart == null)
            {
                cart = new Cart();
                cart.UserId = userId;
                cart.CreatedAt = DateTime.Now;

                if (_cartRepo.CreateUserCart(cart) == false)
                    return CreateResponse(false, "Error With User Cart", null);

                return CreateResponse(true, "Cart Is Empty", null);
            }

            //Cart Already Is Empty 
            if (cart.CartItems.Count == 0)
                return CreateResponse(true, "Cart Is Empty", null);

            cart.CartItems.Clear();
            if (_cartRepo.SaveChanges() == false)
            {
                return CreateResponse(false, "Error In Clear Cart Proccess", null);
            }

            return CreateResponse(true, "Cart Cleared Successfully", null);            
        }

        public GenResponse UpdateCartItem(int cartItemId, UpdateCartItemDTO updateCartItemDTO, string userId)
        {
            try
            {
                Cart cart = _cartRepo.GetCartWithItemsByUserId(userId);
                if (cart == null)
                {
                    cart = new Cart();
                    cart.UserId = userId;
                    cart.CreatedAt = DateTime.Now;

                    if (_cartRepo.CreateUserCart(cart) == false)
                        return CreateResponse(false, "Error With User Cart", null);

                    return CreateResponse(true, "User Cart Is Empty", null);
                }

                CartItem? cartItem = null;//= _cartRepo.GetItemByIdWithProduct(cartItemId);

                foreach (var item in cart.CartItems)
                {
                    if (cartItemId == item.Id)
                    {
                        cartItem = item;
                        break;
                    }
                }

                //cart item id not in the user cart 
                if (cartItem is null)
                    return CreateResponse(false, "Wrong Cart Item Id", updateCartItemDTO);

                if (updateCartItemDTO.Quentity <= cartItem.Product.StockQuantity)
                {
                    cartItem.Quentity = updateCartItemDTO.Quentity;
                    if (_cartRepo.UpdateItem(cartItem) == false)
                        return CreateResponse(false, "Internal Server Error", updateCartItemDTO);

                    return CreateResponse(true, "Cart Item Updated Successfully", updateCartItemDTO);
                }
                else
                {
                    return CreateResponse(false, $"The Quentity Is Greater Than The Stock Quentity:{cartItem.Product.StockQuantity}.", updateCartItemDTO);
                }
            }
            catch (Exception ex)
            {
                return CreateResponse(false, "Internal Server Error", updateCartItemDTO);
            }
        }

        public GenResponse RemoveCartItem(int cartItemId, string? userId)
        {
            try
            {
                Cart cart = _cartRepo.GetCartWithItemsByUserId(userId);
                // user dont have cart
                if (cart is null)
                {
                    cart = new Cart()
                    {
                        UserId = userId,
                        CreatedAt = DateTime.Now
                    };
                    if (_cartRepo.CreateUserCart(cart) == false)
                        return CreateResponse(false, "Internal Server Error", cartItemId);

                    return CreateResponse(false, "User Cart Is Empty", cartItemId);
                }
                //user Have cart

                bool itemFounded = false;
                foreach (var item in cart.CartItems)
                {
                    if(item.Id == cartItemId)
                    {
                        if (_cartRepo.RemoveItem(item) == false)
                            return CreateResponse(false, "Internal Server Error", cartItemId);

                        //*******
                        return CreateResponse(true, "Cart Item Removed Successfully", cartItemId);
                        //*******

                        //itemFounded = true;
                        //cart.CartItems.Remove(item);
                        //if (_cartRepo.SaveChanges() == false)
                        //    return CreateResponse(false, "Internal Server Error", cartItemId);
                        //break;
                    }
                }
                
                return CreateResponse(false, "Cart Item Doesn't Exist In Your Cart", cartItemId);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, "Internal Server Error", cartItemId);
            }
        }

        private GenResponse CreateResponse(bool IsSuccess, string Message, dynamic? DataObject)
        {
            GenResponse response = new GenResponse();
            response.IsSuccess = IsSuccess;
            response.Message = Message;
            response.DataObject = DataObject;
            return response;
        }
    }
}
