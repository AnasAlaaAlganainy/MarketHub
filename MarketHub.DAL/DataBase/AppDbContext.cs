
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
                .WithMany(c => c.Products)
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
        }

    }
}
