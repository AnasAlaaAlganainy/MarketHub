using MarketHub.DAL.DataBase;
using MarketHub.DAL.Entities;
using MarketHub.DAL.Repository.Abstraction;
namespace MarketHub.DAL.Repository.Implementation
{
    public class BrandRepo : IBrandRepo
    {
        private readonly AppDbContext _context;
        public BrandRepo(AppDbContext context)
        {
            _context = context;
        }
        public bool Create(Brand brand)
        {
            try
            {
                _context.Add(brand);
                if (_context.SaveChanges() > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public Brand GetById(int id)
        {
            Brand brand = _context.Brands.Find(id);

            return brand;
        }

        public List<Brand> GetAll() =>
            _context.Brands.ToList();

        //public Brand GetByName(string Name);
        public bool Update(Brand brand)
        {
            try
            {
                _context.Update(brand);

                if (_context.SaveChanges() > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Delete(Brand brand)
        {
            _context.Remove(brand);
            if (_context.SaveChanges() > 0)
                return true;

            return false;
        }
        public bool IsExistsById(int id) =>
            _context.Brands
                .Any(b => b.Id == id);
                    //&& b.IsDeleted == false);

        public bool IsExistsByName(string name) =>
            _context.Brands
                .Any(b => b.Name.ToLower()
                       == name.ToLower());

            //&& b.IsDeleted == false);

        //public bool IsDeletedByName(string name)
        //{
        //    Brand? brand = _context.Brands
        //        .FirstOrDefault(p => p.Name.ToLower()
        //                          == name.ToLower());

        //    return brand.IsDeleted;
        //}
        //public bool IsDeletedById(int id)
        //{
        //    Brand brand = _context.Brands.Find(id);

        //    return brand.IsDeleted;
        //}
        public int NumOfCategoriesWithBrand(int id) =>
            _context.BrandsWithCategories.Count(bc => bc.BrandId == id);

        public int NumOfProductsInBrand(int id) =>
           _context.Products.Count(p => p.BrandId == id);// && p.IsDeleted == false);

        public bool IsMoreBrandWithSameName(int id, string name) =>
            _context.Brands.Any(b => b.Id != id
                                    && b.Name.ToLower() == name.ToLower());
                                    //&& b.IsDeleted == false);
    }
}
