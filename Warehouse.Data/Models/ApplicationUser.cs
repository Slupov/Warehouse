using Microsoft.AspNetCore.Identity;

namespace Warehouse.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual Company Company { get; set; }
        public int? CompanyId { get; set; }
    }
}
