using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Models;
using Warehouse.Services;

namespace Warehouse.Web.Areas.Places.Controllers
{
    public class HomeController : PlacesBaseController
    {
        private readonly IGenericDataService<Place> _places;
        private readonly UserManager<ApplicationUser> _usersManager;

        public HomeController(IGenericDataService<Place> places,
            UserManager<ApplicationUser> usersManager)
        {
            _places = places;
            _usersManager = usersManager;
        }

        // GET: Places/Places
        public async Task<IActionResult> Index()
        {
            var companyId = (await _usersManager.GetUserAsync(User)).Company.Id;

            var vm = await _places.GetListAsync(x => x.CompanyId == companyId);

            return View(vm);
        }

        // GET: Places/Places/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _places.GetSingleOrDefaultAsync(m => m.Id == id);

            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // GET: Places/Places/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Places/Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Place place)
        {
            if (ModelState.IsValid)
            {
                var currUser = await _usersManager.GetUserAsync(User);

                place.Company = currUser.Company;

                _places.Add(place);
                return RedirectToAction(nameof(Index));
            }

            return View(place);
        }

        // GET: Places/Places/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _places.GetSingleOrDefaultAsync(x => x.Id == id);

            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // POST: Places/Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CompanyId")] Place place)
        {
            if (id != place.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _places.Update(place);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await PlaceExists(place.Id)))
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
            return View(place);
        }

        // GET: Places/Places/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _places.GetSingleOrDefaultAsync(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // POST: Places/Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var place = await _places.GetSingleOrDefaultAsync(x => x.Id == id);

            _places.Remove(place);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PlaceExists(int id)
        {
            return await _places.AnyAsync(e => e.Id == id);
        }
    }
}
