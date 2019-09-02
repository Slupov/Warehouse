using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Models;
using Warehouse.Services;

namespace Warehouse.Web.Areas.IncomeExpenses.Controllers
{
    public class HomeController : IncomeExpensesBaseController
    {
        private readonly IGenericDataService<IncomeExpense> _incomeExpenses;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IGenericDataService<IncomeExpense> incomeExpenses,
            UserManager<ApplicationUser> userManager)
        {
            _incomeExpenses = incomeExpenses;
            _userManager    = userManager;
        }

        // GET: IncomeExpenses/IncomeExpenses
        public async Task<IActionResult> Index()
        {
            int companyId = (await _userManager.GetUserAsync(User)).Company.Id;

            var vm = await _incomeExpenses.GetListAsync(i => i.Company.Id == companyId);

            return View(vm);
        }

        // GET: IncomeExpenses/IncomeExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeExpense = await _incomeExpenses.GetSingleOrDefaultAsync(m => m.Id == id);
            if (incomeExpense == null)
            {
                return NotFound();
            }

            return View(incomeExpense);
        }

        // GET: IncomeExpenses/IncomeExpenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IncomeExpenses/IncomeExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncomeExpense incomeExpense)
        {
            if (ModelState.IsValid)
            {
                incomeExpense.Company = (await _userManager.GetUserAsync(User)).Company;

                _incomeExpenses.Add(incomeExpense);
                return RedirectToAction(nameof(Index));
            }

            return View(incomeExpense);
        }

        // GET: IncomeExpenses/IncomeExpenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeExpense = await _incomeExpenses.GetSingleOrDefaultAsync(x => x.Id == id);
            if (incomeExpense == null)
            {
                return NotFound();
            }
            return View(incomeExpense);
        }

        // POST: IncomeExpenses/IncomeExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IncomeExpense incomeExpense)
        {
            if (id != incomeExpense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    incomeExpense.Company = (await _userManager.GetUserAsync(User)).Company;
                    _incomeExpenses.Update(incomeExpense);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await IncomeExpenseExists(incomeExpense.Id))
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
            return View(incomeExpense);
        }

        // GET: IncomeExpenses/IncomeExpenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomeExpense = await _incomeExpenses.GetSingleOrDefaultAsync(m => m.Id == id);
            if (incomeExpense == null)
            {
                return NotFound();
            }

            return View(incomeExpense);
        }

        // POST: IncomeExpenses/IncomeExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incomeExpense = await _incomeExpenses.GetSingleOrDefaultAsync(x => x.Id == id);

            _incomeExpenses.Remove(incomeExpense);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> IncomeExpenseExists(int id)
        {
            return await _incomeExpenses.AnyAsync(e => e.Id == id);
        }
    }
}
