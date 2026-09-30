namespace MarketHub.DAL.Entities
{
    public class AppUser: IdentityUser
    {
        public string? FullName { get; set; }
        public string?  Address { get; set; }

    }
}