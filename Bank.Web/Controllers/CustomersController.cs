using AutoMapper;
using Bank.Application.Services;
using Bank.Infrastructure.Entities;
using Bank.Infrastructure.Identity;
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
        public IActionResult Index(string searchString, int? currentPage)
        {
            int page = currentPage.HasValue ? currentPage.Value : 1;

            int count = _customerService.GetAllCustomersCount();

            int totalPages = (int)Math.Ceiling((double)count / 50);

            var customers = _customerService.GetAllCustomers(searchString, page);

            var model = new CustomerSearchViewModel
            {
                Customers = _mapper.Map<IEnumerable<CustomerViewModel>>(customers),
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult EditCustomer(int id)
        {
            var customer = _customerService.GetCustomer(id);
            var model = _mapper.Map<EditCustomerViewModel>(customer);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
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
            if (succeeded) return RedirectToAction("CustomerProfile", new { id = customer.CustomerId });

            return View(model);
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SearchCustomerProfile(string searchString)
        {
            if (_signInManager.IsSignedIn(User) == false) return RedirectToAction("Login", "User");

            var customer = _customerService.GetCustomerBySearch(searchString);
            if (customer == null) return RedirectToAction("ViewSearchError", "Home");

            return RedirectToAction("CustomerProfile", new { id = customer.CustomerId });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult TopCustomers(string country)
        {
            if (_signInManager.IsSignedIn(User) == false) return RedirectToAction("Login", "User");

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