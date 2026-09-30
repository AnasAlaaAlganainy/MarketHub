using MarketHub.BLL.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.Service.Abstraction
{
    public interface ICategoryService
    {
        public GenResponse Create(CreateCategoryDTO categoryDTO);
        public GenResponse GetById(int id);
        public GenResponse GetAll();
        public GenResponse Update(UpdateCategoryDTO categoryDTO);
        public GenResponse Delete(int id);
    }
}
