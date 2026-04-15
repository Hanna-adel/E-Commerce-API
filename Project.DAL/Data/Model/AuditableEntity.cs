using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
