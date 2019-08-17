using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Data.Models;
using Warehouse.Models;
using Warehouse.Services;

namespace Warehouse.Web.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> _users;

        public HomeController(UserManager<ApplicationUser> users)
        {
            _users = users;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<PartialViewResult> AppSettingsTab()
        {
            var currUser = await _users.GetUserAsync(User);
            int appSettingsId = currUser.Company.ApplicationSettingsId;

            return PartialView("../Views/Shared/Components/AppSettingsTab/AppSettingsTabView.cshtml",
                appSettingsId);
        }
    }
}
