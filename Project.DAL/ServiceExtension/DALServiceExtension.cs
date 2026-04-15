using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Project.DAL
{
    public static class DALServiceExtension
    {
       public static void AddDALServices(this IServiceCollection services, IConfiguration configuration)
       {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options
                .UseSqlServer(connectionString)
                .UseAsyncSeeding(async (context, _, _) =>
                 {
                     if (!await context.Set<Category>().AnyAsync())
                     {
                         var categories = SeedDataProvider.GetCategories();
                         await context.AddRangeAsync(categories);
                         await context.SaveChangesAsync();
                     }

                     if (!await context.Set<Product>().AnyAsync())
                     {
                         var products = SeedDataProvider.GetProducts();
                         await context.AddRangeAsync(products);
                         await context.SaveChangesAsync();
                     }

                 })
                .UseSeeding((context, _) =>
                {
                    if (!context.Set<Category>().Any())
                    {
                        var categories = SeedDataProvider.GetCategories();
                        context.AddRange(categories);
                        context.SaveChanges();
                    }

                    if (!context.Set<Product>().Any())
                    {
                        var products = SeedDataProvider.GetProducts();
                        context.AddRange(products);
                        context.SaveChanges();
                    }
                });
            });   
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
