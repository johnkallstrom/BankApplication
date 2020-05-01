using AutoMapper;
using Bank.Web.Services;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Bank.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;

        public CustomerController(
            IMapper mapper, 
            IAccountService accountService, 
            ICustomerService customerService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult Index()
        {
            var customerList = _customerService.GetAllCustomers();
            var model = _mapper.Map<List<CustomerViewModel>>(customerList);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        [ValidateAntiForgeryToken]
        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult CustomerProfile(int id)
        {
            var customer = _customerService.GetCustomer(id);
            var accounts = _accountService.GetCustomerAccounts(id);

            var model = _mapper.Map<CustomerProfileViewModel>(customer);
            model.Accounts = _mapper.Map<List<AccountViewModel>>(accounts);

            return View(model);
        }
    }
}