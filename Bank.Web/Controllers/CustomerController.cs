using AutoMapper;
using Bank.Infrastructure.Identity;
using Bank.Web.Services;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Bank.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CustomerController(
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
        [AllowAnonymous]
        public IActionResult Search(string searchString)
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