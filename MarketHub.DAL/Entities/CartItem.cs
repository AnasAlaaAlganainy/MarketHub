namespace MarketHub.DAL.Entities
{
    public class CartItem: BaseEntity
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quentity {  get; set; }
        
        //NavBar
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}