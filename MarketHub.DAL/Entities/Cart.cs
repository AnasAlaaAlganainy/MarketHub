namespace MarketHub.DAL.Entities
{
    public class Cart: BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        //NavBar
        public AppUser AppUser { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
