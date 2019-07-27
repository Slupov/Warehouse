using System.Collections.Generic;
using Warehouse.Data.Models;

namespace Warehouse.Web.Areas.CompanyUsers.Models
{
    public class CompanyUsersCreateViewModel
    {
        public ApplicationUser User { get; set; }

        public IEnumerable<string> UserRoles { get; set; }

        public int CompanyId { get; set; }
    }
}
