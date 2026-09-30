namespace MarketHub.DAL.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        
        //Nav Props 
        public List<Product>? Products { get; set; }
        public ICollection<BrandCategory>? CategoryBrands { get; set; }

    }
}
