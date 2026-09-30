using MarketHub.BLL.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.Service.Abstraction
{
    public interface IBrandService
    {
        public GenResponse Create(CreateBrandDTO brandDTO);
        public GenResponse GetById(int id);
        public GenResponse GetAll();
        public GenResponse Update(UpdateBrandDTO brandDTO);
        public GenResponse DeleteById(int id);
    }
}
