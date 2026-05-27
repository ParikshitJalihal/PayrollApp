using HCM.Models.Models;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HCMWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LedgerController : Controller
    {
        private readonly ILedgerService _ledgerService;
        private readonly IAccountService _AccountService;

        public LedgerController(ILedgerService ledgerService, IAccountService accountService)
        {
            _ledgerService = ledgerService;
            _AccountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var entries = await _ledgerService.GetLedgerAsync();
            return View(entries);
        }

        public async Task<IActionResult> Create()
        {
            // Load accounts from the database
            var accounts = await _AccountService.GetAccountsAsync();

            // Populate ViewBag for the dropdown
            ViewBag.Accounts = new SelectList(accounts, "Id", "AccountName");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LedgerEntry entry)
        {
            if (ModelState.IsValid)
            {
                await _ledgerService.AddLedgerEntryAsync(entry);
                return RedirectToAction(nameof(Index));
            }

            // Reload accounts if validation fails
            ViewBag.Accounts = new SelectList(await _ledgerService.GetLedgerAsync(), "Id", "AccountName", entry.AccountId);
            return View(entry);
        }

    }

}
