
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.Service.Abstraction
{
    public interface IProductService
    {
        public GenResponse Create(CreateProductDto productFromReq);
        public GenResponse GetById(int id);
        public GenResponse GetAll();
        public GenResponse Update(UpdateProductDto productFromReq);
        public GenResponse Delete(int id);
    }
}
