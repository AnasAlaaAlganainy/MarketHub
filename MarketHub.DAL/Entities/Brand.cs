using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.DAL.Entities
{
    public class Brand: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        //Nav Props
        public List<Product>? Products { get; set; }
        public ICollection<BrandCategory>? BrandCategories{ get; set; }

    }
}
