

namespace MarketHub.BLL.Service.Implementation
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICartRepo _cartRepo;
        private readonly UserManager<AppUser> _userManager;
        public OrderService(IOrderRepo orderRepo,
                            ICartRepo cartRepo, 
                            UserManager<AppUser> userManager) 
        {
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
            _userManager = userManager;
        }
        //*********************************************************************
        public GenResponse GetById(string userId, int orderId)
        {
            try
            {
                Order userOrder = _orderRepo.GetById(userId,orderId);
                if (userOrder == null)
                    return CreateResponse(false, "You Don't Have This Order.", null);

                //Mapping
                GetOrderByIdDTO orderResponseDto = new GetOrderByIdDTO();

                orderResponseDto.OrderId = userOrder.Id;
                orderResponseDto.ShippingAddress = userOrder.ShippingAddress;
                orderResponseDto.OrderStatus = userOrder.OrderStatus;
                orderResponseDto.OrderDate = userOrder.CreatedAt;
                orderResponseDto.TotalPrice = userOrder.TotalPrice;

                foreach (var item in userOrder.OrderItems)
                {
                    OrderItemResponseDTO orderItemResponseDTO = new OrderItemResponseDTO()
                    {
                        ProductId = item.ProductId ?? 0,
                        Quentity = item.Quentity,
                        ProductName = item.ProductName,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice,
                    };

                    orderResponseDto.OrderItems.Add(orderItemResponseDTO);
                }
                
                return CreateResponse(true, "Here is your Order And Items", orderResponseDto);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, "Internal Server Error.", null);
            }

        }
        //*********************************************************************
        public GenResponse GetAll(string userId, string? status)//Filtering
        {
            try
            {
                List<Order> userOrders = new List<Order>();

                if (!String.IsNullOrEmpty(status))//filter by status
                {
                    List<string> statuses =
                            new List<string> { "pending", "confirmed", "delivered", "processing" };

                    if (!statuses.Contains(status.ToLower()))
                        return CreateResponse(false, "This Status Is Invalid.", status);

                    userOrders = _orderRepo.GetOrdersByStatus(userId, status);
                }
                else
                {
                    userOrders = _orderRepo.GetAll(userId);
                }

                if (userOrders == null) 
                    return CreateResponse(true, "You Don't Have any Orders.", null);

                List<OrderResponseDTO> ordersResponseDTO = new List<OrderResponseDTO>();
                //mapping
                foreach (var order in userOrders)
                {

                    OrderResponseDTO orderResponseDto = new OrderResponseDTO();

                    //orderResponseDto.UserId = userId;
                    orderResponseDto.Id = order.Id;
                    orderResponseDto.ShippingAddress = order.ShippingAddress;
                    orderResponseDto.OrderStatus = order.OrderStatus;
                    orderResponseDto.OrderDate = order.CreatedAt;
                    orderResponseDto.TotalPrice = order.TotalPrice;

                    //foreach (var item in order.OrderItems)
                    //{
                    //    OrderItemResponseDTO orderItemResponseDTO = new OrderItemResponseDTO()
                    //    {
                    //        ProductId = item.ProductId ?? 0,
                    //        Quentity = item.Quentity,
                    //        ProductName = item.ProductName,
                    //        UnitPrice = item.UnitPrice,
                    //        TotalPrice = item.TotalPrice,
                    //    };

                    //    orderResponseDto.OrderItems.Add(orderItemResponseDTO);
                    //}
                    ordersResponseDTO.Add(orderResponseDto);
                }
                return CreateResponse(true, "Here is your Orders", ordersResponseDTO);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, "Internal Server Error.", null);
            }

        }
        //*********************************************************************
        public GenResponse CheckOut(string userId, OrderCreateDTO orderCreateDTO, ClaimsPrincipal User )
        {
            try
            {
                Cart cart = _cartRepo.GetCartWithItemsByUserId(userId);
                //Valids
                if (cart == null)
                {
                    cart = new Cart();
                    cart.UserId = userId;
                    if (_cartRepo.CreateUserCart(cart) == false)
                        return CreateResponse(false, "Internal Server Error", null);
                }
                //if cart is null , or user don't have cart items 
                if (cart.CartItems.Count == 0)
                {
                    return CreateResponse(true, "Your Cart Is Empty", null);
                }

                //Mapping
                //Creating One Order to user

                Order userOrder = new Order();
                userOrder.UserId = userId;
                userOrder.OrderStatus = OrderStatus.Pending.ToString();
                userOrder.PhoneNumber = orderCreateDTO.PhoneNumber;
                userOrder.UserEmail = orderCreateDTO.Email;
                userOrder.ShippingAddress = orderCreateDTO.ShippingAddress;
                userOrder.UserName = _userManager.GetUserName(User);
                userOrder.TotalPrice = 0m;

                //using IDbContextTransaction transaction = _orderRepo.BeginTransaction();
                _orderRepo.BeginTransaction();
                if (_orderRepo.CreateOrder(userOrder) == false)
                {
                    _orderRepo.RollbackTransaction();
                    return CreateResponse(false, "Internal Server Error.", null);
                }

                //creating orderitems and adding it to the order
                List<OrderItem> orderItems = new List<OrderItem>();
                
                foreach (var item in cart.CartItems)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = userOrder.Id;
                    orderItem.ProductId = item.ProductId;
                    orderItem.Quentity = item.Quentity;
                    orderItem.UnitPrice = item.Product.Price;
                    orderItem.TotalPrice = orderItem.Quentity * orderItem.UnitPrice;
                    orderItem.ProductName = item.Product.Name;

                    userOrder.TotalPrice += orderItem.TotalPrice;
                    orderItems.Add(orderItem);
                    //userOrder.OrderItems.Add(orderItem);
                }
                //creating orderitems and saving order TotalPrice
                if (_orderRepo.CreateOrderItems(orderItems) == false)
                {
                    _orderRepo.RollbackTransaction();
                    return CreateResponse(false, "Internal Server Error.", null);
                }
                //clear cart
                cart.CartItems.Clear();

                if (_cartRepo.SaveChanges() == false)
                {
                    _orderRepo.RollbackTransaction();
                    return CreateResponse(false, "Internal Server Error.", null);
                }

                _orderRepo.CommitTransaction();
                return CreateResponse(true, "Order Created Successfully.", null);
            }
            catch (Exception ex)
            {
                _orderRepo.RollbackTransaction();
                return CreateResponse(false, "Internal Server Error.", null);
            }
        }
        //*********************************************************************
        public GenResponse CancelOrder(int orderId, string userId)
        {
            Order userOrder = _orderRepo.GetOrderByIdAndUserId(orderId, userId);
            //Valids
            if (userOrder == null)
                return CreateResponse(false, "No Order With This Id In Your Orders!!.", orderId);
            userOrder.OrderStatus = "Canceled";
            if (_orderRepo.SaveChanges() == false)
                return CreateResponse(false, "Internal Server Error.", orderId);

            return CreateResponse(true, "Order Deleted Successfully.", orderId);
        }
        //*********************************************************************
        public GenResponse UpdateStatus(string userId, int orderId, UpdateOrderStatusDTO updateOrderStatusDTO)
        {
            Order userOrder = _orderRepo.GetOrderByIdAndUserId(orderId, userId);
            //Valids
            if (userOrder == null)
                return CreateResponse(true, "No Order With That Id Belongs To You ( Wrong Id).", null);
            
            string status = updateOrderStatusDTO.OrderStatus.ToLower();
            
            List<string> statuses =
                new List<string> { "Pending", "Canceled", "Confirmed", "Delivered", "Processing" };
            
            //if(!statuses.Contains(status))
            //{
            //    return CreateResponse(false, "This Status Is Invalid.", updateOrderStatusDTO);
            //}

            bool found = false;
            for (int i = 0; i < statuses.Count; i++)
            {
                if (status == statuses[i].ToLower())
                {
                    found = true;
                }
            }
            if(found == false)
                return CreateResponse(false, "This Status Is Invalid.", updateOrderStatusDTO);

            userOrder.OrderStatus = status;

            if(_orderRepo.SaveChanges() == false)
                return CreateResponse(false, "Internal Server Error.", orderId);

            return CreateResponse(true, "Status Changed Successfully.", status);
        }
        //*********************************************************************
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
