using AutoMapper;
using Bank.Web.Services;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerController(IMapper mapper, ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Cashier")]
        [ValidateAntiForgeryToken]
        public IActionResult Search(CustomerSearchViewModel model)
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult CustomerProfile(int id)
        {
            var selectedCustomer = _customerService.GetCustomerByID(id);
            var model = _mapper.Map<CustomerProfileViewModel>(selectedCustomer);

            return View(model);
        }
    }
}