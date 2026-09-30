using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.DAL.Repository.Abstraction
{
    public interface IBrandRepo
    {
        public bool Create(Brand brand);
        public Brand GetById(int id);
        public List<Brand> GetAll();
        //public Brand GetByName(string Name);
        public bool Update(Brand brand);
        public bool Delete(Brand brand);
        //public bool IsDeletedByName(string name);
        //public bool IsDeletedById(int id);
        public bool IsExistsById(int id);
        public bool IsExistsByName(string name);
        public int NumOfCategoriesWithBrand(int id);
        public int NumOfProductsInBrand(int id);
        public bool IsMoreBrandWithSameName(int id, string name);
    }
}
