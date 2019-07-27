using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Data.Models;
using Warehouse.Services;

namespace Warehouse.Web.Areas.Settings.Controllers
{
    public class HomeController : SettingsBaseController
    {
        private readonly IGenericDataService<ApplicationSettings> _appSettings;
        private readonly IGenericDataService<Data.Models.Company> _companies;
        private readonly UserManager<ApplicationUser> _userManager;


        public HomeController(
            IGenericDataService<ApplicationSettings> appSettings,
            IGenericDataService<Data.Models.Company> companies,
            UserManager<ApplicationUser> userManager)
        {
            _appSettings = appSettings;
            _companies   = companies;
            _userManager = userManager;
        }

        // GET: Settings/ApplicationSettings
        public async Task<IActionResult> Index()
        {
            return View(await _appSettings.GetAllAsync());
        }

        // GET: Settings/ApplicationSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationSettings = await _appSettings
                .GetSingleOrDefaultAsync(m => m.Id == id);
            
            if (applicationSettings == null)
            {
                return NotFound();
            }

            return View(applicationSettings);
        }

        // GET: Settings/ApplicationSettings/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Settings/ApplicationSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationSettings appSetttings)
        {
            if (ModelState.IsValid)
            {
                var currUser = await _userManager.GetUserAsync(User);
                var userCompany = _companies.GetSingleOrDefault(x => x.Id == currUser.CompanyId);

                appSetttings.Company = userCompany;
                appSetttings.CompanyId = userCompany.Id;

                // add appSettings to the database
                _appSettings.Add(appSetttings);

                // get appSettings new id from the db and attach it to company
                userCompany.ApplicationSettings = appSetttings;
                userCompany.ApplicationSettingsId = appSetttings.Id;

                _companies.Update(userCompany);

                return RedirectToAction(nameof(Index));
            }

            return View(appSetttings);
        }

        // GET: Settings/ApplicationSettings/Edit/5
        public async Task<IActionResult> Edit(int? companyId)
        {
            if (companyId == null)
            {
                return NotFound();
            }

            var currSettings = await _appSettings.GetSingleOrDefaultAsync(x => x.Company.Id == companyId);

            if (currSettings == null)
            {
                return NotFound();
            }

            return View(currSettings);
        }

        // POST: Settings/ApplicationSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicationSettings applicationSettings)
        {
            if (id != applicationSettings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _appSettings.Update(applicationSettings);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await ApplicationSettingsExists(applicationSettings.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                //TODO Stoyan Lupov 26 July, 2019 Make path to home dynamic
                return LocalRedirect("/");
            }

            return View(applicationSettings);
        }

        // GET: Settings/ApplicationSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currUser = await _userManager.GetUserAsync(User);

            var currSettings = await _appSettings.GetSingleOrDefaultAsync(
                x => x.CompanyId == currUser.CompanyId);

            if (currSettings == null)
            {
                return NotFound();
            }

            return View(currSettings);
        }

        // POST: Settings/ApplicationSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationSettings = await _appSettings.GetSingleOrDefaultAsync(
                x => x.Id == id);

            _appSettings.Remove(applicationSettings);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ApplicationSettingsExists(int id)
        {
            return await _appSettings.AnyAsync(e => e.Id == id);
        }
    }
}
