using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Data.Models;
using Warehouse.Web.Models;

namespace Warehouse.Web.Views.Shared.Components
{
    public class OperationsTabViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _users;

        public OperationsTabViewComponent(UserManager<ApplicationUser> users)
        {
            _users = users;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currUser = await _users.GetUserAsync(Request.HttpContext.User);

            OperationsTabViewModel vm = new OperationsTabViewModel();

            var result = (IViewComponentResult)View("OperationsTabView", vm);

            return result;
        }
    }
}
