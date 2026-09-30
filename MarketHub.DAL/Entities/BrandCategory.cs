namespace MarketHub.DAL.Entities
{
    public class BrandCategory
    {
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        //Nav Props
        public Brand? Brand { get; set; }
        public Category? Category { get; set; }

    }
}
