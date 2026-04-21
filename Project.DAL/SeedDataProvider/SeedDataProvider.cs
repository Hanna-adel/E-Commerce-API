using Microsoft.EntityFrameworkCore;

namespace Project.DAL
{
    public static class SeedDataProvider
    {
        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Raw Honey",
                    ImageUrl = "placeholder.jpg",
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Category
                {
                    Id = 2,
                    Name = "Flavored Honey",
                    ImageUrl = "placeholder.jpg",
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Category
                {
                    Id = 3,
                    Name = "Honey Accessories",
                    ImageUrl = "placeholder.jpg",
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                }
            };
        }

        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Sidr Honey",
                    Description = "Pure Sidr honey harvested from the mountains of Yemen",
                    Price = 49.99m,
                    StockQuantity = 100,
                    ImageUrl = "placeholder.jpg",
                    IsAvailable = true,
                    CategoryId = 1,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Product
                {
                    Id = 2,
                    Name = "Manuka Honey",
                    Description = "Premium New Zealand Manuka honey MGO 400+",
                    Price = 59.99m,
                    StockQuantity = 80,
                    ImageUrl = "placeholder.jpg",
                    IsAvailable = true,
                    CategoryId = 1,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Product
                {
                    Id = 3,
                    Name = "Wildflower Honey",
                    Description = "100% natural wildflower honey cold extracted",
                    Price = 19.99m,
                    StockQuantity = 150,
                    ImageUrl = "placeholder.jpg",
                    IsAvailable = true,
                    CategoryId = 1,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Product
                {
                    Id = 4,
                    Name = "Clover Honey",
                    Description = "Pure clover honey mild and sweet flavor",
                    Price = 14.99m,
                    StockQuantity = 200,
                    ImageUrl = "placeholder.jpg",
                    IsAvailable = true,
                    CategoryId = 1,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Product
                {
                    Id = 5,
                    Name = "Cinnamon Honey",
                    Description = "Natural honey infused with Ceylon cinnamon",
                    Price = 24.99m,
                    StockQuantity = 90,
                    ImageUrl = "placeholder.jpg",
                    IsAvailable = true,
                    CategoryId = 2,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Product
                {
                    Id = 6,
                    Name = "Ginger Honey",
                    Description = "Raw honey blended with fresh ginger root",
                    Price = 22.99m,
                    StockQuantity = 85,
                    ImageUrl = "placeholder.jpg",
                    IsAvailable = true,
                    CategoryId = 2,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Product
                {
                    Id = 7,
                    Name = "Lavender Honey",
                    Description = "Delicate honey infused with organic lavender",
                    Price = 26.99m,
                    StockQuantity = 70,
                    ImageUrl = "placeholder.jpg",
                    IsAvailable = true,
                    CategoryId = 2,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Product
                {
                    Id = 8,
                    Name = "Chili Honey",
                    Description = "Sweet honey with a spicy chili kick",
                    Price = 23.99m,
                    StockQuantity = 60,
                    ImageUrl = "placeholder.jpg",
                    IsAvailable = true,
                    CategoryId = 2,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Product
                {
                    Id = 9,
                    Name = "Wooden Honey Dipper",
                    Description = "Handcrafted wooden honey dipper 20cm",
                    Price = 9.99m,
                    StockQuantity = 300,
                    ImageUrl = "placeholder.jpg",
                    IsAvailable = true,
                    CategoryId = 3,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Product
                {
                    Id = 10,
                    Name = "Glass Honey Jar",
                    Description = "Premium glass jar with airtight lid 500ml",
                    Price = 7.99m,
                    StockQuantity = 250,
                    ImageUrl = "placeholder.jpg",
                    IsAvailable = true,
                    CategoryId = 3,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                },
                new Product
                {
                    Id = 11,
                    Name = "Honey Gift Set",
                    Description = "Set of 3 premium honey jars in a gift box",
                    Price = 49.99m,
                    StockQuantity = 50,
                    ImageUrl = "placeholder.jpg",
                    IsAvailable = true,
                    CategoryId = 3,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = null
                }
            };
        }
        public static List<ApplicationRole> GetRoles()
        {
            return new List<ApplicationRole>
    {
        new ApplicationRole
        {
            Id = "1",
            Name = "Admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = "1"  
        },
        new ApplicationRole
        {
            Id = "2",
            Name = "User",
            NormalizedName = "USER",
            ConcurrencyStamp = "2"  
        }
    };
        }
    }
}