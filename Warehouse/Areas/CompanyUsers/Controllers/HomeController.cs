using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Warehouse.Data.Models;
using Warehouse.Web.Areas.CompanyUsers.Models;
using Warehouse.Web.Areas.Identity.Pages.Account;

namespace Warehouse.Web.Areas.CompanyUsers.Controllers
{
    public class HomeController : CompanyUsersBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public HomeController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger      = logger;
        }

        public async Task<IActionResult> Index(int companyId)
        {
            //TODO Stoyan Lupov 26 July, 2019 Checks if current user is COMPANY OWNER
            CompanyUsersIndexViewModel vm = new CompanyUsersIndexViewModel();

            vm.Users = await _userManager.Users.Where(x => x.Company.Id == 
                 companyId).ToListAsync();


            var userRoles = new List<string>();

            foreach (var usr in vm.Users)
            {
                IEnumerable<string> currentRolesArr = (await _userManager.GetRolesAsync(usr));
                userRoles.Add(String.Join(", ", currentRolesArr));
            }

            vm.UserRoles = userRoles;
            
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new CompanyUsersCreateEditViewModel();
            vm.User = (await _userManager.GetUserAsync(User));

            return View(vm);
        }

        // POST: IncomeExpenses/IncomeExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyUsersCreateEditViewModel editViewModel)
        {
            //TODO Stoyan Lupov 26 July, 2019 Checks if current user is COMPANY OWNER

            if (ModelState.IsValid)
            {
                editViewModel.User.Company = (await _userManager.GetUserAsync(User)).Company;

                //TODO Stoyan Lupov 26 July, 2019 Implement random password generator
                var password = "123";
                var result = await _userManager.CreateAsync(editViewModel.User, password);

                if (result.Succeeded)
                {
                    //add user to roles
                    foreach (var role in editViewModel.UserRoles)
                    {
                        //check if its a valid role name
                        if (!string.IsNullOrEmpty(role) && 
                            await _roleManager.RoleExistsAsync(role))
                        {
                            await _userManager.AddToRoleAsync(editViewModel.User, role);
                        }        
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(editViewModel.User);

                    string confirmationLink = Url.Page(
                        "/Account/ConfirmEmail",
                        null, // handler
                        new { area = "Identity", userid = editViewModel.User.Id, code = code },
                        Request.Scheme);

                    await _emailSender.SendEmailAsync(editViewModel.User.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.");

                    return RedirectToAction(nameof(Index),
                        new { companyId = editViewModel.User.Company.Id });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(editViewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var vm  = new CompanyUsersCreateEditViewModel();
            vm.User = user;

            vm.UserRoles = await _userManager.GetRolesAsync(user);

            return View(vm);
        }

        // POST: IncomeExpenses/IncomeExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, CompanyUsersCreateEditViewModel vm)
        {
            if (id != vm.User.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.PhoneNumber = vm.User.PhoneNumber;

                    await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExistsAsync(vm.User.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index),
                    new { companyId = vm.User.Company.Id });
            }

            return View(vm);
        }

        private bool UserExistsAsync(string id)
        {
            return _userManager.Users.Any(x => (0 == String.CompareOrdinal(x.Id, id)));
        }
    }
}