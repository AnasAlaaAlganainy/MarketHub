using MarketHub.DAL.Enums;
using System.ComponentModel;

namespace MarketHub.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? UserName { get; set; }
        public string? UserEmail {  get; set; }
        public string? ShippingAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        //nav props
        public AppUser? AppUser { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
