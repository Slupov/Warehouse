using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Models;
using Warehouse.Services;

namespace Warehouse.Web.Areas.Orders.Controllers
{
    public class HomeController : OrdersBaseController
    {
        private readonly IGenericDataService<Order> _orders;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IGenericDataService<Order> orders,
            UserManager<ApplicationUser> userManager)
        {
            _orders    = orders;
            _userManager = userManager;
        }

        // GET: Orders/Home
        public async Task<IActionResult> Index(int companyId)
        {
            var vm = await _orders.GetListAsync(x => x.Company.Id == companyId);

            return View(vm);
        }

        // GET: Orders/Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orders.GetSingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Company = (await _userManager.GetUserAsync(User)).Company;

                _orders.Add(order);

                return RedirectToAction(nameof(Index), 
                    new {companyId = order.Company.Id});
            }

            return View(order);
        }

        // GET: Orders/Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orders.GetSingleOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.Company = (await _userManager.GetUserAsync(User)).Company;
                    _orders.Update(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OrderExistsAsync(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index), 
                    new { companyId = order.Company.Id });
            }
            return View(order);
        }

        // GET: Orders/Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orders.GetSingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order   = await _orders.GetSingleOrDefaultAsync(x => x.Id == id);
            var companyId = order.Company.Id;
            
            _orders.Remove(order);
            return RedirectToAction(nameof(Index),
                new { companyId = companyId });
        }

        private async Task<bool> OrderExistsAsync(int id)
        {
            return await _orders.AnyAsync(e => e.Id == id);
        }
    }
}
