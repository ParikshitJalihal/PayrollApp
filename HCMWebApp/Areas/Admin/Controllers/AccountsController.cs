using HCM.Models.Models;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HCMWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        public readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<IActionResult> Index()
        {
            // Fetch accounts from your service
            var accounts = await _accountService.GetAccountsAsync();

            // Pass the accounts list to the view
            return View(accounts);
        }

        public IActionResult Create()
        {
            Account account = new Account();
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            if (ModelState.IsValid)
            {
                await _accountService.AddAccountAsync(account);
                return RedirectToAction(nameof(Index));
            }
            return View(account);

        }
    }
}
