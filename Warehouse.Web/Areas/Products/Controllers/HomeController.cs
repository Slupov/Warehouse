using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Models;
using Warehouse.Services;
using Warehouse.Services.Media;
using Warehouse.Web.Areas.Products.Models;

namespace Warehouse.Web.Areas.Products.Controllers
{
    public class HomeController : ProductsBaseController
    {
        private readonly IGenericDataService<Product> _products;
        private readonly UserManager<ApplicationUser> _users;
        private readonly IMediaTransferer _mediaTransferer;

        public HomeController(IGenericDataService<Product> products,
            IMediaTransferer mediaTransferer,
            UserManager<ApplicationUser> users)
        {
            _mediaTransferer = mediaTransferer;
            _products = products;
            _users = users;
        }

        // GET: Products/Home
        public async Task<IActionResult> Index()
        {
            int companyId = (await _users.GetUserAsync(User)).Company.Id;

            var currProducts =
                await _products.GetListAsync(x => x.Company.Id == companyId);

            var vm = (currProducts).Select(x => new ProductIndexViewModel()
            {
                Product = x,
                ThumbnailPath = (_mediaTransferer
                        .GetProductPhotosFilesRelative(x).GetAwaiter()
                        .GetResult())
                    .FirstOrDefault()
            });

            return View(vm);
        }

        // GET: Products/Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =
                await _products.GetSingleOrDefaultAsync(m => m.Id == id);
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

        private async Task<ProductEditCreateViewModel>
            GenerateProductCreateEditViewModel()
        {
            var vm = new ProductEditCreateViewModel();
            vm.Product = new Product();
            vm.Product.Company = (await _users.GetUserAsync(User)).Company;

            vm.Places = new List<SelectListItem>();
            vm.MeasureUnits = new List<SelectListItem>();
            vm.ProductGroups = new List<SelectListItem>();
            vm.OutProductPhotosPaths = new List<string>();

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
        public async Task<IActionResult> Create(ProductEditCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Product.Company = (await _users.GetUserAsync(User)).Company;

                _products.Add(vm.Product);

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Products/Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =
                await _products.GetSingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var vm = await GenerateProductCreateEditViewModel();
            vm.Product = product;

            //delete
            await _mediaTransferer.GetProductPhotosFiles(product);

            vm.OutProductPhotosPaths =
                (await _mediaTransferer.GetProductPhotosFilesRelative(product));

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

                return RedirectToAction(nameof(Index));
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

            var product =
                await _products.GetSingleOrDefaultAsync(m => m.Id == id);
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
            var product =
                await _products.GetSingleOrDefaultAsync(x => x.Id == id);
            var companyId = product.Company.Id;

            _products.Remove(product);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _products.Any(e => e.Id == id);
        }
    }
}