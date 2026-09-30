using MarketHub.DAL.DataBase;
using MarketHub.DAL.Entities;
using MarketHub.DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
namespace MarketHub.DAL.Repository.Implementation
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }

        public List<Product>? GetAll()
        {
            List<Product> products = _context.Products.Include(p => p.Brand).Include(p => p.Category).ToList();
            return products;
        }

        public Product? GetById(int id)
        {
            Product? product = _context.Products.Include(p => p.Brand).Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            
            return product;
        }
        public bool Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                //Product prdct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //}

        public bool Update(Product product)
        {
            try
            {
                _context.Products.Update(product);

                if (_context.SaveChanges() > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(Product product)
        {
            try
            {
                //product.IsDeleted = true;
                _context.Products.Remove(product);

                if (_context.SaveChanges() > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsExistsById(int id)=>
            _context.Products
            .Any(p => p.Id == id);
        public bool IsExistsByName(string name) =>
            _context.Products
            .Any(p => p.Name.ToLower() == name.ToLower());

        //public bool IsDeletedByName(string name) =>
        //    _context.Products
        //    .Any(p => p.Name.ToLower() == name.ToLower());
        //امسحها 
    }

}