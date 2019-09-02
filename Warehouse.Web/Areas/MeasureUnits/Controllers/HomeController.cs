using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Data.Models;
using Warehouse.Services;

namespace Warehouse.Web.Areas.MeasureUnits.Controllers
{
    public class HomeController : MeasureUnitsBaseController
    {
        private readonly IGenericDataService<MeasureUnit> _measureUnits;
        private readonly UserManager<ApplicationUser> _users;

        public HomeController(IGenericDataService<MeasureUnit> measureUnits, UserManager<ApplicationUser> users)
        {
            _measureUnits = measureUnits;
            _users        = users;
        }

        // GET: MeasureUnits/MeasureUnits
        public async Task<IActionResult> Index()
        {
            int companyId = (await _users.GetUserAsync(User)).Company.Id;

            var vm = await _measureUnits.GetListAsync(x => x.Company.Id == companyId);

            return View(vm);
        }

        // GET: MeasureUnits/MeasureUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measureUnit = await _measureUnits.GetSingleOrDefaultAsync(m => m.Id == id);
            if (measureUnit == null)
            {
                return NotFound();
            }

            return View(measureUnit);
        }

        // GET: MeasureUnits/MeasureUnits/Create
        public  IActionResult Create()
        {
            return View();
        }

        // POST: MeasureUnits/MeasureUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeasureUnit measureUnit)
        {
            if (ModelState.IsValid)
            {
                var currUser = await _users.GetUserAsync(User);
                measureUnit.Company = currUser.Company;

                _measureUnits.Add(measureUnit);

                return RedirectToAction(nameof(Index));
            }

            return View(measureUnit);
        }

        // GET: MeasureUnits/MeasureUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measureUnit = await _measureUnits.GetSingleOrDefaultAsync(x => x.Id == id);

            if (measureUnit == null)
            {
                return NotFound();
            }

            return View(measureUnit);
        }

        // POST: MeasureUnits/MeasureUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MeasureUnit measureUnit)
        {
            if (id != measureUnit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _measureUnits.Update(measureUnit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasureUnitExists(measureUnit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(measureUnit);
        }

        // GET: MeasureUnits/MeasureUnits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measureUnit = await _measureUnits.GetSingleOrDefaultAsync(m => m.Id == id);

            if (measureUnit == null)
            {
                return NotFound();
            }

            return View(measureUnit);
        }

        // POST: MeasureUnits/MeasureUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var measureUnit = await _measureUnits.GetSingleOrDefaultAsync(x => x.Id == id);

            var companyId = measureUnit.CompanyId;


            _measureUnits.Remove(measureUnit);

            return RedirectToAction(nameof(Index));
        }

        private bool MeasureUnitExists(int id)
        {
            return _measureUnits.Any(e => e.Id == id);
        }
    }
}
