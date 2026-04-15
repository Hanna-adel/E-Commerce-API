using Microsoft.AspNetCore.Identity;

namespace Project.DAL
{
    public class ApplicationUser : IdentityUser
    {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public Cart? Cart { get; set; }
            public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
