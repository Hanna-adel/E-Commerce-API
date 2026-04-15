using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class Category:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        // Navigation
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
