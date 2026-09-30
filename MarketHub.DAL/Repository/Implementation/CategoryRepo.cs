using MarketHub.DAL.Entities;

namespace MarketHub.DAL.Repository.Implementation
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _context;
        public CategoryRepo(AppDbContext context)
        {
            this._context = context;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = _context.Categories.ToList();
            if (categories.Count > 0)
            {
                return categories;
            }

            return null;
        }
        public Category GetById(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            
            if (category == null)
            {
                return null;
            }
            return category;
        }
        public Category GetByName(string Name)=>
            _context.Categories.FirstOrDefault(c => c.Name.ToLower() == Name.ToLower());
        public bool Create(Category category)
        {
            try
            {
                _context.Categories.Add(category);

                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                return false;
            }

        }
        public bool Update(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                if (_context.SaveChanges() > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
                //category.IsDeleted = true;

                _context.Categories.Remove(category);
                if (_context.SaveChanges() > 0)
                    return true;

                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int NumOfProductsInCategory(int id) =>
             _context.Products.Count(p => p.CategoryId == id);

        public int NumOfBrandsInCategory(int id) =>
            _context.BrandsWithCategories.Count(bc => bc.CategoryId == id );

        public bool IsExistsById(int id) =>
            _context.Categories.Any(c => c.Id == id);

        public bool IsExistsByName(string name) =>
            _context.Categories.Any(c => c.Name.ToLower() == name.ToLower());

        //public bool IsDeletedByName(string name)
        //{
        //    Category? category = _context.Categories
        //        .FirstOrDefault(p => p.Name.ToLower()
        //                            == name.ToLower());

        //    return category.IsDeleted;
        //}

        //public bool IsDeletedById(int id)
        //{
        //    Category? category = _context
        //        .Categories.FirstOrDefault(c => c.Id == id);
        //    //if (category == null)
        //    //{ 
                
        //    //}
        //    return category.IsDeleted;
        //}

        public bool IsMoreCategoryWithSameName(int id, string name)
            => _context.Categories.Any(c => c.Id != id 
                                    && c.Name.ToLower()
                                     == name.ToLower());
            //Id=1      Name=books
            //Id=5      Name=books
            //false
    }
}
