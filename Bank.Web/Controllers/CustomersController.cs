using AutoMapper;
using Bank.Application.Services.Interfaces;
using Bank.Infrastructure.Entities;
using Bank.Infrastructure.Identity;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

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

        #region Action Methods
        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        [Route("customers")]
        public IActionResult Index(string sortOrder, string currentFilter, string searchQuery, int? page)
        {
            var customers = _customerService.GetAllCustomers(sortOrder, currentFilter, searchQuery);

            if (searchQuery != null)
            {
                page = 1;
            }
            else
            {
                searchQuery = currentFilter;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);

            var pagedCustomerList = customers.ToPagedList(pageNumber, pageSize);

            var customerViewModelList = _mapper.Map<IEnumerable<Customers>, IEnumerable<CustomerViewModel>>(pagedCustomerList.ToArray());

            var model = new CustomerListViewModel
            {
                Customers = new StaticPagedList<CustomerViewModel>(customerViewModelList, pagedCustomerList.GetMetaData()),
                SearchQuery = searchQuery,
                CurrentFilter = searchQuery,
                SortingParameters = GetSortingParameters(sortOrder)
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
        #endregion

        #region Private Methods
        private CustomerListSortingParameters GetSortingParameters(string sortOrder)
        {
            return new CustomerListSortingParameters
            {
                Current = sortOrder,
                Name = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty,
                Address = sortOrder == "address" ? "address_desc" : "address",
                Country = sortOrder == "country" ? "country_desc" : "country",
                City = sortOrder == "city" ? "city_desc" : "city"
            };
        }
        #endregion
    }
}