using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Models;
using Warehouse.Services;
using Warehouse.Web.Areas.Products.Models;

namespace Warehouse.Web.Areas.Products.Controllers
{
    public class HomeController : ProductsBaseController
    {
        private readonly IGenericDataService<Product> _products;
        private readonly UserManager<ApplicationUser> _users;

        public HomeController(IGenericDataService<Product> products,
            UserManager<ApplicationUser> users)
        {
            _products = products;
            _users    = users;
        }

        // GET: Products/Home
        public async Task<IActionResult> Index(int? companyId)
        {
            if (companyId is null)
            {
                return NotFound();
            }

            var vm = await _products.GetListAsync(x => x.Company.Id == companyId);

            return View(vm);
        }

        // GET: Products/Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _products.GetSingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Home/Create
        public async Task<IActionResult> Create()
        {
            var vm = await GenerateProductCreateEditViewModel();

            return View(vm);
        }

        private async Task<ProductEditCreateViewModel> GenerateProductCreateEditViewModel()
        {
            var vm = new ProductEditCreateViewModel();
            vm.Product = new Product();

            vm.Places = new List<SelectListItem>();
            vm.MeasureUnits = new List<SelectListItem>();
            vm.ProductGroups = new List<SelectListItem>();

            var company = (await _users.GetUserAsync(User)).Company;

            foreach (var companyPlace in company.Places)
            {
                vm.Places.Add(new SelectListItem
                {
                    Text = companyPlace.Name,
                    Value = companyPlace.Id.ToString()
                });
            }

            foreach (var measureUnit in company.MeasureUnits)
            {
                vm.MeasureUnits.Add(new SelectListItem
                {
                    Text = measureUnit.Name,
                    Value = measureUnit.Id.ToString()
                });
            }

            foreach (var prodGroup in company.ProductGroups)
            {
                vm.ProductGroups.Add(new SelectListItem
                {
                    Text = prodGroup.Name,
                    Value = prodGroup.Id.ToString()
                });
            }

            return vm;
        }

        // POST: Products/Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Company = (await _users.GetUserAsync(User)).Company;
                
                _products.Add(product);
                return RedirectToAction(nameof(Index), new {companyId = product.Company.Id});
            }

            return View(product);
        }

        // GET: Products/Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _products.GetSingleOrDefaultAsync(x=> x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var vm = await GenerateProductCreateEditViewModel();
            vm.Product = product;

            return View(vm);
        }

        // POST: Products/Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _products.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { companyId = product.CompanyId });
            }
            return View(product);
        }

        // GET: Products/Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _products.GetSingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product   = await _products.GetSingleOrDefaultAsync(x => x.Id == id);
            var companyId = product.Company.Id;

            _products.Remove(product);
            return RedirectToAction(nameof(Index), new {companyId = companyId});
        }

        private bool ProductExists(int id)
        {
            return _products.Any(e => e.Id == id);
        }
    }
}
