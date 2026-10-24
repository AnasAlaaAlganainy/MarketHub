namespace MarketHub.DAL.Repository.Abstraction
{
    public interface IOrderRepo
    {
        public List<Order> GetAll(string userId);
        public List<Order> GetOrdersByStatus(string userId, string status);
        public Order GetById(string userId, int orderId);
        public Order GetOrderByIdAndUserId(int orderId, string userId);
        public bool CreateOrder(Order userOrder);
        public bool CreateOrderItems(List<OrderItem> orderItems);
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
        public bool SaveChanges();
    }
}
