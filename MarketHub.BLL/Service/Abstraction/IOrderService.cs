namespace MarketHub.BLL.Service.Abstraction
{
    public interface IOrderService
    {
        public GenResponse CheckOut(string userId, OrderCreateDTO orderCreateDTO, ClaimsPrincipal User);
        public GenResponse GetAll(string userId, string? status);
        public GenResponse GetById(string userId, int orderId);
        public GenResponse CancelOrder(int orderId, string userId);
        public GenResponse UpdateStatus(string userId, int orderId, UpdateOrderStatusDTO updateOrderStatusDTO);
    }
}
