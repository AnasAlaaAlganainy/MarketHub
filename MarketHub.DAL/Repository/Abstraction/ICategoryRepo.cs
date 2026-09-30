using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.DAL.Repository.Abstraction
{
    public interface ICategoryRepo
    {
        //CRUD
        public List<Category> GetAll();
        public Category GetById(int id);
        public Category GetByName(string Name);
        public bool Create(Category category);
        public bool Update(Category category);
        public bool Delete(int id);
        //
        public int NumOfProductsInCategory(int id);
        public int NumOfBrandsInCategory(int id);
        public bool IsExistsById(int id);
        public bool IsExistsByName(string name);
        //public bool IsDeletedByName(string name);
        //public bool IsDeletedById(int id);
        public bool IsMoreCategoryWithSameName(int id, string name);

    }
}
