using AutoMapper;
using Bank.Web.Exceptions;
using Bank.Web.Services;
using Bank.Web.Services.Account;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult AccountDetails(int id, int? currentPage)
        {
            var account = _accountService.GetAccount(id);
            var transactions = _accountService.GetAccountTransactions(id);

            var model = new AccountDetailsViewModelBuilder()
                .WithAccount(account)
                .WithTransactions(transactions)
                .WithPaging(currentPage)
                .Build();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult Previous(int id, int? currentPage)
        {
            var account = _accountService.GetAccount(id);
            var transactions = _accountService.GetAccountTransactions(id);

            var model = new AccountDetailsViewModelBuilder()
                .WithAccount(account)
                .WithTransactions(transactions)
                .WithPaging(currentPage)
                .Build();

            return ViewComponent("TransactionList", model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult Next(int id, int? currentPage)
        {
            var account = _accountService.GetAccount(id);
            var transactions = _accountService.GetAccountTransactions(id);

            var model = new AccountDetailsViewModelBuilder()
                .WithAccount(account)
                .WithTransactions(transactions)
                .WithPaging(currentPage)
                .Build();

            return ViewComponent("TransactionList", model);
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult Deposit(int id)
        {
            var account = _accountService.GetAccount(id);
            var model = _mapper.Map<CreateDepositViewModel>(account);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        public async Task<IActionResult> Deposit(CreateDepositViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var succeeded = await _accountService.Deposit(model.AccountId, model.Amount);
                if (succeeded) return RedirectToAction(nameof(AccountDetails), new { id = model.AccountId });
            } 
            catch(AccountNotFoundException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult Withdrawal(int id)
        {
            var account = _accountService.GetAccount(id);
            var model = _mapper.Map<CreateWithdrawalViewModel>(account);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        public async Task<ActionResult> Withdrawal(CreateWithdrawalViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var succeeded = await _accountService.Withdrawal(model.AccountId, model.Amount);
                if (succeeded) return RedirectToAction(nameof(AccountDetails), new { id = model.AccountId });
            }
            catch (AccountNotFoundException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }
            catch (InsufficientFundsException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult Transfer(int id)
        {
            var account = _accountService.GetAccount(id);
            var model = _mapper.Map<CreateTransferViewModel>(account);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        public async Task<IActionResult> Transfer(CreateTransferViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var succeeded = await _accountService.Transfer(model.FromAccountId, model.ToAccountId, model.Amount);
                if (succeeded) return RedirectToAction("AccountDetails", new { id = model.FromAccountId });
            }
            catch (AccountNotFoundException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }
            catch (InsufficientFundsException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }
            catch (MatchingAccountsException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }

            return View(model);
        }
    }
}