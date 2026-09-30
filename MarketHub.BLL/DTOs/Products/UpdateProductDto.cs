using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Products
{
    public class UpdateProductDto
    {
        [Required(ErrorMessage = "Product Id Is Required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name Is Required")]
        [MaxLength(150, ErrorMessage = "Product Name Must Be between 1 -> 150 Letters")]
        public string Name { get; set; }

        [MaxLength(350, ErrorMessage = "Product Description Must Be between 0 -> 350 Letters")]
        public string? Description { get; set; }

        [Range(0.01,double.MaxValue,ErrorMessage = $"Product Price Must Be between 0.01 -> 100,000")]
        public decimal Price { get; set; }

        [Range(0,int.MaxValue,ErrorMessage = $"You Must Enter Stock Quantity and it Must Be Greater than 1 ")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Category Id Is Required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Brand Id Is Required")]
        public int BrandId { get; set; }
        //public string? CategoryName { get; set; }
        //public string? BrandName { get; set; }

    }
}
