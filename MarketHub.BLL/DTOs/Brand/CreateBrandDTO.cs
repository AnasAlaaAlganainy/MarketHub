using System.ComponentModel.DataAnnotations;

namespace MarketHub.BLL.DTOs.Brand
{
    public class CreateBrandDTO
    {
        [Required(ErrorMessage ="Name Is Required")]
        [StringLength(150,ErrorMessage = "Name Must Be Between 1 -> 150")]
        public string Name { get; set; }
        
        [StringLength(350, ErrorMessage = "Description Must Be Between 0 -> 350")]
        public string? Description { get; set; }

    }
}
