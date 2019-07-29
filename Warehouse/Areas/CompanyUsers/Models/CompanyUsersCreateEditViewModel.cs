using System.Collections.Generic;
using Warehouse.Data.Models;

namespace Warehouse.Web.Areas.CompanyUsers.Models
{
    public class CompanyUsersCreateEditViewModel
    {
        public ApplicationUser User { get; set; }

        public IEnumerable<string> UserRoles { get; set; }
    }
}
