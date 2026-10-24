using MarketHub.DAL.Utils.Abstraction;
using MarketHub.DAL.Utils.Implementation;
using Microsoft.EntityFrameworkCore.Design;

namespace MarketHub.DAL.DI
{
    public static class AddDIConfigurationForDAL 
    {
        public static IServiceCollection DIConfigForDAL(this IServiceCollection services )
        {
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICategoryRepo,CategoryRepo>();
            services.AddScoped<IBrandRepo, BrandRepo>();
            services.AddScoped<ISeedData, SeedData>();
            services.AddScoped<ICartRepo, CartRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            //services.AddIdentity<AppUser, IdentityRole>()
            //    .AddEntityFrameworkStores<AppDbContext>();
            return services;
        }
    }
}
