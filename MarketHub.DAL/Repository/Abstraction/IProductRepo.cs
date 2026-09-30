namespace MarketHub.DAL.Repository.Abstraction
{
    public interface IProductRepo
    {
        List<Product>? GetAll();
        Product? GetById(int id);
        bool Create(Product product);
        bool Delete(Product product);
        bool Update(Product product);
        bool IsExistsById(int id);
        bool IsExistsByName(string name);
        //bool IsDeleted(string name); //امسحها 
    }
}
