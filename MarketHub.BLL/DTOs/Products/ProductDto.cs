using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Products
{
    public class ProductDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int? BrandId { get; set; }
        public string? BrandName { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

    }
}
