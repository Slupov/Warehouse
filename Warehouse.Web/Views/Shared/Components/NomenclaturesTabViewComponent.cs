using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Data.Models;
using Warehouse.Web.Models;

namespace Warehouse.Web.Views.Shared.Components
{
    public class NomenclaturesTabViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _users;

        public NomenclaturesTabViewComponent(UserManager<ApplicationUser> users)
        {
            _users = users;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currUser = await _users.GetUserAsync(Request.HttpContext.User);

            NomenclaturesTabViewModel vm = new NomenclaturesTabViewModel();

            if (currUser.Company is null)
            {
                vm.ShouldVisualize = false;
            }
            else
            {
                vm.ShouldVisualize = true;
                vm.CompanyId = currUser.Company.Id;
            }

            var result = (IViewComponentResult)View("NomenclaturesTabView", vm);
            return result;
        }
    }
}
