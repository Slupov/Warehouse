using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Models;
using Warehouse.Services;

namespace Warehouse.Web.Areas.BankAccounts.Controllers
{
    public class HomeController : BankAccountsBaseController
    {
        private readonly IGenericDataService<BankAccount> _bankAcccounts;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IGenericDataService<BankAccount> bankAcccounts,
            UserManager<ApplicationUser> userManager)
        {
            _bankAcccounts = bankAcccounts;
            _userManager   = userManager;
        }

        // GET: BankAccounts/BankAccounts
        public async Task<IActionResult> Index(int? companyId)
        {
            if (companyId is null)
            {
                return NotFound();
            }

            var vm = await _bankAcccounts.GetListAsync(x => x.Company.Id == companyId);

            return View(vm);
        }

        // GET: BankAccounts/BankAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _bankAcccounts.GetSingleOrDefaultAsync(m => m.Id == id);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/BankAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankAccounts/BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                var currUser = await _userManager.GetUserAsync(User);

                bankAccount.Company = currUser.Company;

                _bankAcccounts.Add(bankAccount);

                return RedirectToAction(nameof(Index), new { companyId = bankAccount.Company.Id });
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/BankAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _bankAcccounts.GetSingleOrDefaultAsync(x=> x.Id == id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BankAccount bankAccount)
        {
            if (id != bankAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bankAcccounts.Update(bankAccount);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await BankAccountExists(bankAccount.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {companyId = bankAccount.CompanyId});
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/BankAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _bankAcccounts
                .GetSingleOrDefaultAsync(m => m.Id == id);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankAccount = await _bankAcccounts.GetSingleOrDefaultAsync(x => x.Id == id);
            var companyId = bankAccount.Company.Id;

            _bankAcccounts.Remove(bankAccount);

            return RedirectToAction(nameof(Index), new { companyId = companyId });
        }

        private async Task<bool> BankAccountExists(int id)
        {
            return await _bankAcccounts.AnyAsync(e => e.Id == id);
        }
    }
}
