using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Data.Models;
using Warehouse.Web.Models;

namespace Warehouse.Web.Views.Shared
{
    public class CompanyInfoTabViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _users;

        public CompanyInfoTabViewComponent(UserManager<ApplicationUser> users)
        {
            _users = users;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currUser = await _users.GetUserAsync(Request.HttpContext.User);

            CompanyInfoTabViewModel vm = new CompanyInfoTabViewModel();

            if (currUser.Company is null)
            {
                vm.ShouldVisualize = false;
            }
            else
            {
                vm.ShouldVisualize = true;
            }

            var result = (IViewComponentResult) View("CompanyInfoTabView", vm);
            return result;
        }
    }
}