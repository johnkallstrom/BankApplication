using AutoMapper;
using Bank.Application.Exceptions;
using Bank.Application.Services;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("accounts/details/{id}")]
        public IActionResult AccountDetails(int id, int? startPosition)
        {
            var account = _accountService.GetAccount(id);
            var transactions = _accountService.GetAccountTransactions(id, startPosition);

            var model = _mapper.Map<AccountDetailsViewModel>(account);
            model.Transactions = _mapper.Map<List<TransactionViewModel>>(transactions);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult LoadTransactions(int id, int? startPosition)
        {
            var account = _accountService.GetAccount(id);
            var transactions = _accountService.GetAccountTransactions(id, startPosition);

            var model = _mapper.Map<AccountDetailsViewModel>(account);
            model.Transactions = _mapper.Map<List<TransactionViewModel>>(transactions);

            return ViewComponent("TransactionList", model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("accounts/deposit/{id}")]
        public IActionResult Deposit(int id)
        {
            var account = _accountService.GetAccount(id);
            var model = _mapper.Map<CreateDepositViewModel>(account);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("accounts/deposit/{id}")]
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
                model.Balance = _accountService.GetAccount(model.AccountId).Balance;
                return View(model);
            }
            catch (NoNegativeAmountException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                model.Balance = _accountService.GetAccount(model.AccountId).Balance;
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("accounts/withdrawal/{id}")]
        public IActionResult Withdrawal(int id)
        {
            var account = _accountService.GetAccount(id);
            var model = _mapper.Map<CreateWithdrawalViewModel>(account);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("accounts/withdrawal/{id}")]
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
                model.Balance = _accountService.GetAccount(model.AccountId).Balance;
                return View(model);
            }
            catch (InsufficientFundsException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                model.Balance = _accountService.GetAccount(model.AccountId).Balance;
                return View(model);
            }
            catch (NoNegativeAmountException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                model.Balance = _accountService.GetAccount(model.AccountId).Balance;
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("accounts/transfer/{id}")]
        public IActionResult Transfer(int id)
        {
            var account = _accountService.GetAccount(id);
            var model = _mapper.Map<CreateTransferViewModel>(account);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("accounts/transfer/{id}")]
        public async Task<IActionResult> Transfer(CreateTransferViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var succeeded = await _accountService.Transfer(model.FromAccountId, model.ToAccountId, model.Amount);
                if (succeeded) return RedirectToAction(nameof(AccountDetails), new { id = model.FromAccountId });
            }
            catch (AccountNotFoundException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                model.Balance = _accountService.GetAccount(model.FromAccountId).Balance;
                return View(model);
            }
            catch (InsufficientFundsException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                model.Balance = _accountService.GetAccount(model.FromAccountId).Balance;
                return View(model);
            }
            catch (MatchingAccountsException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                model.Balance = _accountService.GetAccount(model.FromAccountId).Balance;
                return View(model);
            }
            catch (NoNegativeAmountException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                model.Balance = _accountService.GetAccount(model.FromAccountId).Balance;
                return View(model);
            }

            return View(model);
        }
    }
}