using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Data.Models;
using Warehouse.Web.Models;

namespace Warehouse.Web.Views.Shared
{
    public class CompanyAdministrationTabViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _users;

        public CompanyAdministrationTabViewComponent(UserManager<ApplicationUser> users)
        {
            _users = users;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currUser = await _users.GetUserAsync(Request.HttpContext.User);

            CompanyAdministrationTabViewModel vm = new CompanyAdministrationTabViewModel();

            if (currUser.Company is null || currUser.Company.ApplicationSettings is null)
            {
                vm.ShouldVisualize = false;
            }
            else
            {
                vm.ShouldVisualize = true;
                vm.CompanyId = currUser.Company.Id;
            }

            var result = (IViewComponentResult)View("CompanyAdministrationTabView", vm);
            return result;
        }
    }
}
