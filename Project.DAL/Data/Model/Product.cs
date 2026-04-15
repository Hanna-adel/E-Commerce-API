using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class Product:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;


        // FK
        public int CategoryId { get; set; }

        // Navigation
        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
