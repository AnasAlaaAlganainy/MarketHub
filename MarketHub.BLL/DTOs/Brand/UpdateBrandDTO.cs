using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Brand
{
    public class UpdateBrandDTO
    {
        [Required(ErrorMessage ="Id Is Required")]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name Is Required")]
        [MaxLength(150)]
        public string NewName { get; set; }

        [MaxLength(350)]
        public string? NewDescription { get; set; }
        
    }
}
