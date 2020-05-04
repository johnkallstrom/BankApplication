using AutoMapper;
using Bank.Web.Services;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult AccountDetails(int id)
        {
            var account = _accountService.GetAccount(id);
            var transactions = _accountService.GetAccountTransactions(id);

            var model = _mapper.Map<AccountDetailsViewModel>(account);
            model.Transactions = _mapper.Map<List<TransactionViewModel>>(transactions);

            return View(model);
        }
    }
}