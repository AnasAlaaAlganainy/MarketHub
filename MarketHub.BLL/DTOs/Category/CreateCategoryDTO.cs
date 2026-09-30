using System.ComponentModel.DataAnnotations;

namespace MarketHub.BLL.DTOs.Category
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Category Name Is Required")]
        public string Name { get; set; }
        
        [MaxLength(350)]
        public string? Description { get; set; }

    }
}
