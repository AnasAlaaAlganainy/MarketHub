using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Category
{
    public class UpdateCategoryDTO
    {
        [Required(ErrorMessage = "Category Id Is Required")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Category Name Is Required")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Category Name Must Be Between 1 --> 150 Characters")]
        public string NewName { get; set; }
        
        
        [MaxLength(350, ErrorMessage = "Category Description Must Be Between 0 --> 350 Characters, So Don't Exceed the 350 Characters")]
        public string NewDescription { get; set; }
        
    }
}
