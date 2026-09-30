using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Products
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }


        [MaxLength(350)]
        public string? Description { get; set; }


        [Required]
        [Range(0.01,double.MaxValue)]
        public decimal Price { get; set; }


        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }


        [Required]
        [Range(0, int.MaxValue)]
        public int BrandId { get; set; }


        [Required]
        [Range(0, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
