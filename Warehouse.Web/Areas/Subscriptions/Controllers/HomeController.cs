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

namespace Warehouse.Web.Areas.Subscriptions.Controllers
{
    public class HomeController : SubscriptionsBaseController
    {
        private readonly IGenericDataService<Subscription> _subscriptions;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IGenericDataService<Subscription> subscriptions,
            UserManager<ApplicationUser> userManager)
        {
            _subscriptions = subscriptions;
            _userManager   = userManager;
        }

        // GET: Subscriptions/Home
        public async Task<IActionResult> Index(int companyId)
        {
            var vm = await _subscriptions.GetListAsync(s => s.Company.Id == companyId);

            return View(vm);
        }

        // GET: Subscriptions/Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _subscriptions.GetSingleOrDefaultAsync(m => m.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // GET: Subscriptions/Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subscriptions/Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                subscription.Company = (await _userManager.GetUserAsync(User)).Company;

                subscription.RequestedOn = DateTime.Now;

                //TODO Stoyan Lupov 27 July, 2019 Set this whenever the payment has been completed
                subscription.PayedOn     = DateTime.Now;

                _subscriptions.Add(subscription);
                return RedirectToAction(nameof(Index), new { companyId = subscription.Company.Id });
            }

            return View(subscription);
        }

        // GET: Subscriptions/Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _subscriptions.GetSingleOrDefaultAsync(x => x.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }
            return View(subscription);
        }

        // POST: Subscriptions/Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subscription subscription)
        {
            if (id != subscription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    subscription.Company = (await _userManager.GetUserAsync(User)).Company;

                    _subscriptions.Update(subscription);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SubscriptionExists(subscription.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { companyId = subscription.Company.Id });
            }
            return View(subscription);
        }

        // GET: Subscriptions/Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _subscriptions.GetSingleOrDefaultAsync(m => m.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // POST: Subscriptions/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscription = await _subscriptions.GetSingleOrDefaultAsync(x => x.Id == id);
            var companyId    = subscription.Company.Id;
            _subscriptions.Remove(subscription);

            return RedirectToAction(nameof(Index), new { companyId = companyId });
        }

        private async Task<bool> SubscriptionExists(int id)
        {
            return await _subscriptions.AnyAsync(e => e.Id == id);
        }
    }
}
