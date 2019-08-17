using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Data.Models;
using Warehouse.Web.Models;

namespace Warehouse.Web.Views.Shared
{
    public class AppSettingsTabViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _users;

        public AppSettingsTabViewComponent(UserManager<ApplicationUser> users)
        {
            _users = users;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currUser = await _users.GetUserAsync(Request.HttpContext.User);

            AppSettingsTabViewModel vm = new AppSettingsTabViewModel();

            if (currUser.Company is null || currUser.Company.ApplicationSettings is null)
            {
                vm.ShouldVisualize = false;
            }
            else
            {
                vm.ShouldVisualize = true;
                vm.AppSettingsId   = currUser.Company.ApplicationSettings.Id;
            }

            var result = (IViewComponentResult)View("AppSettingsTabView", vm);
            return result;
        }
    }
}
