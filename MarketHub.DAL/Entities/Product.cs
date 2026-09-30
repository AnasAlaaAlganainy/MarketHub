using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.DAL.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // FK
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        
        // Navigation Props
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }

    }
}
