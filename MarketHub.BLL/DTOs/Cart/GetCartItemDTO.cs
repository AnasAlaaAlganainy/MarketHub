namespace MarketHub.BLL.DTOs.Cart
{
    public class GetCartItemDTO
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quentity { get; set; }


    }
}