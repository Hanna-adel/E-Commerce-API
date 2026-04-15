using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        // FKs
        public int ProductId { get; set; }
        public int CartId { get; set; }



        // Navigation
        public Product Products { get; set; }
        public Cart Carts { get; set; }
    }
}
