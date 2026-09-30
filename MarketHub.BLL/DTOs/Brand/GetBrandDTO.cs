using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Brand
{
    public class GetBrandDTO
    {  
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? NumOfProducts { get; set; }
        public int? NumOfCategories { get; set; }


    }
}
