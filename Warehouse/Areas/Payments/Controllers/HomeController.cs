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
using Warehouse.Web.Areas.Payments.Models;

namespace Warehouse.Web.Areas.Payments.Controllers
{
    public class HomeController : PaymentsBaseController
    {
        private readonly IGenericDataService<Payment> _payments;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IGenericDataService<Payment> payments,
            UserManager<ApplicationUser> userManager)
        {
            _payments    = payments;
            _userManager = userManager;
        }

        // GET: Payments/Home
        public async Task<IActionResult> Index(int companyId)
        {
            var vm = await _payments.GetListAsync(x => x.Company.Id == companyId);

            return View(vm);
        }

        // GET: Payments/Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _payments.GetSingleOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payments/Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.Company = (await _userManager.GetUserAsync(User)).Company;

                _payments.Add(payment);

                return RedirectToAction(nameof(Index), 
                    new {companyId = payment.Company.Id});
            }

            return View(payment);
        }

        // GET: Payments/Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _payments.GetSingleOrDefaultAsync(x => x.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    payment.Company = (await _userManager.GetUserAsync(User)).Company;
                    _payments.Update(payment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PaymentExistsAsync(payment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index), 
                    new { companyId = payment.Company.Id });
            }
            return View(payment);
        }

        // GET: Payments/Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _payments.GetSingleOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment   = await _payments.GetSingleOrDefaultAsync(x => x.Id == id);
            var companyId = payment.Company.Id;
            
            _payments.Remove(payment);
            return RedirectToAction(nameof(Index),
                new { companyId = companyId });
        }

        private async Task<bool> PaymentExistsAsync(int id)
        {
            return await _payments.AnyAsync(e => e.Id == id);
        }
    }
}
