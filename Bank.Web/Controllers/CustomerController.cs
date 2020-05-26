using AutoMapper;
using Bank.Application.Services;
using Bank.Application.Services.Search;
using Bank.Infrastructure.Entities;
using Bank.Infrastructure.Identity;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IAzureSearchService _azureSearchService;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CustomerController(
            IAzureSearchService azureSearchService,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,  
            ICustomerService customerService)
        {
            _azureSearchService = azureSearchService;
            _signInManager = signInManager;
            _mapper = mapper;
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public async Task<IActionResult> Index(string searchString, int? currentPage)
        {
            var page = currentPage.HasValue ? currentPage.Value : 1;
            var searchResults = await _azureSearchService.RunQueryAsync(searchString, page);
            var totalPages = (int)Math.Ceiling((double)searchResults.Count / 50);

            var customers = _customerService.GetCustomersByIndex(searchResults);

            var model = new AzureSearchViewModel
            {
                SearchString = searchString,
                CurrentPage = page,
                TotalPages = totalPages,
                Customers = _mapper.Map<List<CustomerViewModel>>(customers)
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
            await _azureSearchService.MergeOrUploadCustomersAsync(_customerService.GetAllCustomers());
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
            await _azureSearchService.MergeOrUploadCustomersAsync(_customerService.GetAllCustomers());
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
            if (customer == null) return RedirectToAction(nameof(SearchErrorResult));

            return RedirectToAction("CustomerProfile", new { id = customer.CustomerId });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SearchErrorResult()
        {
            return View();
        }
    }
}