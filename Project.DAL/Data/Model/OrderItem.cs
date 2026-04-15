using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // FKs
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation
        public Product Products { get; set; }
        public Order Orders { get; set; }
    }
}
