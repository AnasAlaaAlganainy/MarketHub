namespace MarketHub.BLL.DI
{
    public static class AddDIConfigurationForBLL
    {
        public static IServiceCollection DIConfigForBLL(this IServiceCollection services )
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            //services.AddIdentityCore<>
            return services;
        }
    }
}
