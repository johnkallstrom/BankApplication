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
        public IActionResult Index(int? currentPage, string searchWord)
        {
            var customers = _customerService.GetAllCustomers();

            var model = new CustomerListViewModelBuilder()
                .WithCustomers(customers)
                .WithSearch(searchWord)
                .WithPaging(currentPage)
                .Build();

            return View(model);
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