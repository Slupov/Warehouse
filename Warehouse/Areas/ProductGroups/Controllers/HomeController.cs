using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Models;
using Warehouse.Services;

namespace Warehouse.Web.Areas.ProductGroup.Controllers
{
    public class HomeController : ProductGroupsBaseController
    {
        private readonly IGenericDataService<Data.Models.ProductGroup> _productGroups;
        private readonly UserManager<ApplicationUser> _users;

        public HomeController(IGenericDataService<Data.Models.ProductGroup> productGroups, UserManager<ApplicationUser> users)
        {
            _productGroups = productGroups;
            _users         = users;
        }

        // GET: ProductGroup/ProductGroups
        public async Task<IActionResult> Index(int? companyId)
        {
            var vm = await _productGroups.GetListAsync(p => p.Company.Id == companyId);

            return View(vm);
        }

        // GET: ProductGroup/ProductGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _productGroups.GetSingleOrDefaultAsync(m => m.Id == id);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // GET: ProductGroup/ProductGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductGroup/ProductGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Data.Models.ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                var currUser = await _users.GetUserAsync(User);
                productGroup.Company = currUser.Company;

                _productGroups.Add(productGroup);

                return RedirectToAction(nameof(Index), new { companyId = productGroup.Company.Id });
            }
       
            return View(productGroup);
        }

        // GET: ProductGroup/ProductGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _productGroups.GetSingleOrDefaultAsync(x => x.Id == id);
            if (productGroup == null)
            {
                return NotFound();
            }
            
            return View(productGroup);
        }

        // POST: ProductGroup/ProductGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Data.Models.ProductGroup productGroup)
        {
            if (id != productGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productGroups.Update(productGroup);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductGroupExists(productGroup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index), new { companyId = productGroup.CompanyId });
            }

            return View(productGroup);
        }

        // GET: ProductGroup/ProductGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _productGroups.GetSingleOrDefaultAsync(m => m.Id == id);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // POST: ProductGroup/ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productGroup = await _productGroups.GetSingleOrDefaultAsync(x => x.Id == id);
            var companyId = productGroup.Company.Id;

            _productGroups.Remove(productGroup);

            return RedirectToAction(nameof(Index), new { companyId = companyId });
        }

        private bool ProductGroupExists(int id)
        {
            return _productGroups.Any(e => e.Id == id);
        }
    }
}
