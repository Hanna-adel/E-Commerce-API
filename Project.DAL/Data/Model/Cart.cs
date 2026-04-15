using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; }


        // Navigation
        public ApplicationUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
