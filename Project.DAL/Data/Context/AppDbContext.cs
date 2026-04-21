using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Project.DAL
{
    public class AppDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(): base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditLog();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void AuditLog()
        {
            var dateTime = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = dateTime;

                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = dateTime;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Category>().HasData(SeedDataProvider.GetCategories());

            modelBuilder.Entity<Product>().HasData(SeedDataProvider.GetProducts());
            modelBuilder.Entity<ApplicationRole>().HasData(SeedDataProvider.GetRoles());
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }

    }
}
