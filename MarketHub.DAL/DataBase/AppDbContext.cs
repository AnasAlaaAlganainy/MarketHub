
namespace MarketHub.DAL.DataBase
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        { } 
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandCategory> BrandsWithCategories { get; set; }
        public DbSet<Cart> Carts {  get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            //Product
            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();
            
            //modelBuilder.Entity<Product>()
            //    .Property(p => p.CreatedAt);

            modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2)
            .HasColumnName("Price");

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(350);

            //Category

            modelBuilder.Entity<Category>()
                .ToTable("Categories")
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .HasMaxLength(350);

            //Brand

            modelBuilder.Entity<Brand>()
                .ToTable("Brands")
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Brand>()
                .Property(b => b.Name)
                .HasMaxLength(150);

            modelBuilder.Entity<Brand>()
                .Property(b => b.Description)
                .HasMaxLength(350);

       
            
            //BrandCategory

            modelBuilder.Entity<BrandCategory>()
                .HasKey(bc => new { bc.BrandId, bc.CategoryId });

            modelBuilder.Entity<BrandCategory>()
                .HasOne(bc => bc.Brand)
                .WithMany(b => b.BrandCategories)
                .HasForeignKey(b => b.BrandId);

            modelBuilder.Entity<BrandCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.CategoryBrands)
                .HasForeignKey(b => b.CategoryId);

            //Cart 
            modelBuilder.Entity<Cart>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.AppUser)
                .WithOne()
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //CartItems 
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => ci.Id);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            //Order
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.AppUser)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .Property(o => o.PhoneNumber)
                .HasMaxLength(15);

            modelBuilder.Entity<Order>()
                .Property(o => o.UserEmail)
                .HasMaxLength(255);// characters

            modelBuilder.Entity<Order>()
                .Property(o => o.UserName)
                .HasMaxLength(30);//characters

            modelBuilder.Entity<Order>()
                .Property("ShippingAddress")
                .HasMaxLength(255);//characters

            //OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);
            
            //ديه العلاقة بين الاوردر والاعناصر بتاعته العلاقتين مع بعض هنا من غير ما اروح اعملها هناك
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
           
        }

    }
}
