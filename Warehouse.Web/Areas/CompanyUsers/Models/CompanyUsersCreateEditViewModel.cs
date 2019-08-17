using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Data.Models;

namespace Warehouse.Web.Areas.CompanyUsers.Models
{
    public class CompanyUsersCreateEditViewModel
    {
        public ApplicationUser User { get; set; }

        public List<SelectListItem> UserRoles { get; set; }
        public string[] SelectedUserRoles { get; set; }
    }
}
