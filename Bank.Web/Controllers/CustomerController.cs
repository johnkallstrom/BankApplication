﻿using AutoMapper;
using Bank.Infrastructure.Entities;
using Bank.Web.Services;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public CustomerController(
            IUserService userService,
            IMapper mapper,  
            ICustomerService customerService)
        {
            _userService = userService;
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
        public IActionResult CreateCustomer()
        {
            var model = new CreateCustomerViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var customer = _mapper.Map<Customers>(model);

            var succeeded = await _customerService.CreateCustomer(customer);
            if (succeeded) RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Search(string searchString)
        {
            if (_userService.IsUserLoggedIn() == false) return RedirectToAction("Login", "User");

            var customer = _customerService.GetCustomerBySearch(searchString);
            if (customer == null) return RedirectToAction(nameof(SearchErrorResult));

            return RedirectToAction("CustomerProfile", new { id = customer.CustomerId });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SearchErrorResult()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult CustomerProfile(int id)
        {
            var customer = _customerService.GetCustomer(id);
            var accounts = _customerService.GetCustomerAccounts(id);

            var model = _mapper.Map<CustomerProfileViewModel>(customer);
            model.Accounts = _mapper.Map<List<AccountViewModel>>(accounts);

            return View(model);
        }
    }
}