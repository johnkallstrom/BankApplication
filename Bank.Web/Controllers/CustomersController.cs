﻿using AutoMapper;
using Bank.Application.Services;
using Bank.Application.Services.Interfaces;
using Bank.Infrastructure.Entities;
using Bank.Infrastructure.Identity;
using Bank.Web.Pagination;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CustomersController(
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,  
            ICustomerService customerService)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("customers/{query?}")]
        public IActionResult Index(string sortOrder, string searchQuery)
        {
            var customers = _customerService.GetAllCustomers(sortOrder, searchQuery);

            var model = new CustomerListViewModel
            {
                Customers = _mapper.Map<IEnumerable<CustomerViewModel>>(customers),
                SearchQuery = searchQuery,
                NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty,
                AddressSortParam = sortOrder == "address" ? "address_desc" : "address",
                CountrySortParam = sortOrder == "country" ? "country_desc" : "country",
                CitySortParam = sortOrder == "city" ? "city_desc" : "city"
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("customers/edit/{id}")]
        public IActionResult EditCustomer(int id)
        {
            var customer = _customerService.GetCustomer(id);
            var model = _mapper.Map<EditCustomerViewModel>(customer);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("customers/edit/{id}")]
        public async Task<IActionResult> EditCustomer(EditCustomerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var customer = _mapper.Map<Customers>(model);

            var succeeded = await _customerService.EditCustomer(customer);
            if (succeeded) return RedirectToAction("CustomerProfile", new { id = customer.CustomerId });

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("customers/create")]
        public IActionResult CreateCustomer()
        {
            var model = new CreateCustomerViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("customers/create")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var customer = _mapper.Map<Customers>(model);

            var succeeded = await _customerService.CreateCustomer(customer);
            if (succeeded) return RedirectToAction("CustomerProfile", new { id = customer.CustomerId });

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("customers/{id}")]
        public IActionResult CustomerProfile(int id)
        {
            var customer = _customerService.GetCustomer(id);
            var accounts = _customerService.GetCustomerAccounts(id);

            var model = _mapper.Map<CustomerProfileViewModel>(customer);
            model.Accounts = _mapper.Map<List<AccountViewModel>>(accounts);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customers/search")]
        public IActionResult SearchCustomerProfile(string searchString)
        {
            if (_signInManager.IsSignedIn(User) == false) return RedirectToAction("Login", "Users");

            var customer = _customerService.GetCustomerBySearch(searchString);
            if (customer == null) return RedirectToAction("ViewSearchError", "Home");

            return RedirectToAction("CustomerProfile", new { id = customer.CustomerId });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customers/top/{country}")]
        public IActionResult TopCustomers(string country)
        {
            if (_signInManager.IsSignedIn(User) == false) return RedirectToAction("Login", "Users");

            var customers = _customerService.GetTopCustomersByCountry(country);

            var model = new TopCustomerListViewModel
            {
                Country = country,
                Customers = _mapper.Map<List<CustomerViewModel>>(customers)
            };

            return View(model);
        }
    }
}