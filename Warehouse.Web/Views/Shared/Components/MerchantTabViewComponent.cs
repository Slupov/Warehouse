using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Data.Models;
using Warehouse.Web.Models;

namespace Warehouse.Web.Views.Shared.Components
{
    public class MerchantTabViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _users;

        public MerchantTabViewComponent(UserManager<ApplicationUser> users)
        {
            _users = users;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currUser = await _users.GetUserAsync(Request.HttpContext.User);

            MerchantTabViewModel vm = new MerchantTabViewModel();

            var result = (IViewComponentResult)View("MerchantTabView", vm);
            return result;
        }
    }
}
