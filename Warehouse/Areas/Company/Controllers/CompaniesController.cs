using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Models;
using Warehouse.Services;
using Warehouse.Services.ApiServices;

namespace Warehouse.Web.Areas.Company.Controllers
{
    public class CompaniesController : CompaniesBaseController
    {
        private readonly IGenericDataService<Data.Models.Company> _companies;
        private readonly IMerchantRegistryService _merchantRegistry;
        private readonly UserManager<ApplicationUser> _userManager;

        public CompaniesController(IGenericDataService<Data.Models.Company> companies,
            IMerchantRegistryService merchantRegistry,
            UserManager<ApplicationUser> userManager)
        {
            _companies = companies;
            _merchantRegistry = merchantRegistry;
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
            Data.Models.Company c = new Data.Models.Company();
            c.Contacts = new Contacts();
            c.Contacts.Email = (await _userManager.GetUserAsync(User)).Email;
            
            return View(c);
        }

        // POST: Company/Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Data.Models.Company company)
        {
            if (ModelState.IsValid)
            {
                company.Contacts.Company = company;
                var currUser = await _userManager.GetUserAsync(User);

                if (company.ApplicationUsers is null)
                {
                    company.ApplicationUsers = new List<ApplicationUser>();
                }

                company.ApplicationUsers.Add(currUser);

                _companies.Add(company);

                var returnUrl = "/settings/applicationsettings/create";
                return LocalRedirect(returnUrl);
            }

            return View(company);
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
