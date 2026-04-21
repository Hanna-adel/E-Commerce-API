using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus status { get; set; } = 0;
        public string ShippingAddress { get; set; }
        public DateTime PlacedAt { get; set; } = DateTime.Now;

        // FK
        public string UserId { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
