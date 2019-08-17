using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Web.Areas.CompanyUsers.Models
{
    public class CompanyUsersIndexViewModel
    {
        public IEnumerable<Warehouse.Data.Models.ApplicationUser> Users { get; set; }

        public IEnumerable<string> UserRoles{ get; set; }
    }
}
