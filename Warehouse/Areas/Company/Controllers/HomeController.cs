using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Models;
using Warehouse.Services;
using Warehouse.Services.ApiServices;
using Warehouse.Services.Media;
using Warehouse.Web.Areas.Company.Models;

namespace Warehouse.Web.Areas.Company.Controllers
{
    public class HomeController : CompaniesBaseController
    {
        private readonly IGenericDataService<Data.Models.Company> _companies;
        private readonly IMediaTransferer _mediaTransferer;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IGenericDataService<Data.Models.Company> companies,
            IMediaTransferer mediaTransferer,
            UserManager<ApplicationUser> userManager)
        {
            _companies = companies;
            _mediaTransferer = mediaTransferer;
            _userManager = userManager;
        }

        // GET: Company/Companies
        public async Task<IActionResult> Index()
        {
            return View(await _companies.GetAllAsync());
        }

        // GET: Company/Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companies.GetSingleOrDefaultAsync(m => m.Id == id);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        //TODO Stoyan Lupov 23 July, 2019 Remove method create (it is created programatically)
        // GET: Company/Companies/Create
        public async Task<IActionResult> Create()
        {
            CompanyCreateViewModel vm = new CompanyCreateViewModel();
            vm.Company = new Data.Models.Company();

            vm.Company.Contacts = new Contacts();
            vm.Company.Contacts.Email = (await _userManager.GetUserAsync(User)).Email;
            
            return View(vm);
        }

        // POST: Company/Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Company.Contacts.Company = vm.Company;
                vm.Company.ApplicationSettings = new ApplicationSettings();
                vm.Company.ApplicationSettings.Company = vm.Company;

                var currUser = await _userManager.GetUserAsync(User);

                if (vm.Company.ApplicationUsers is null)
                {
                    vm.Company.ApplicationUsers = new List<ApplicationUser>();
                }

                vm.Company.ApplicationUsers.Add(currUser);

                _companies.Add(vm.Company);

                //add photo to file system
                _mediaTransferer.UploadCompanyLogo(vm.Company, vm.CompanyAvatar);

                //TODO Stoyan Lupov 23 July, 2019 Make path dynamically
                var returnUrl = "/settings/home/edit?companyId=" + vm.Company.ApplicationSettings.Id;
                return LocalRedirect(returnUrl);
            }

            return View(vm);
        }

        // GET: Company/Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companies.GetSingleOrDefaultAsync(x => x.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Company/Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IdentificationCode,ContactsId,OwnerName,ApplicationSettingsId")] Data.Models.Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _companies.Update(company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await CompanyExistsAsync(company.Id)))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Company/Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companies.GetSingleOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Company/Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _companies.GetSingleOrDefaultAsync(x => x.Id == id);
            _companies.Remove(company);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CompanyExistsAsync(int id)
        {
            return await _companies.AnyAsync(e => e.Id == id);
        }
    }
}
