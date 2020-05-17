using Bank.Web.Services;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Bank.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBankStatisticsService _bankStatisticsService;

        public HomeController(
            IUserService userService,
            IBankStatisticsService bankStatisticsService)
        {
            _userService = userService;
            _bankStatisticsService = bankStatisticsService;
        }

        [HttpGet]
        [AllowAnonymous]
        //[ResponseCache(CacheProfileName = "Default")]
        public IActionResult Index()
        {
            var model = new BankStatisticsViewModel
            {
                TotalCustomersInSweden = _bankStatisticsService.GetCountryCustomerStatistics("Sweden"),
                TotalAccountsInSweden = _bankStatisticsService.GetCountryAccountStatistics("Sweden"),
                TotalBalanceInSweden = _bankStatisticsService.GetTotalBalanceByCountry("Sweden"),

                TotalCustomersInDenmark = _bankStatisticsService.GetCountryCustomerStatistics("Denmark"),
                TotalAccountsInDenmark = _bankStatisticsService.GetCountryAccountStatistics("Denmark"),
                TotalBalanceInDenmark = _bankStatisticsService.GetTotalBalanceByCountry("Denmark"),

                TotalCustomersInNorway = _bankStatisticsService.GetCountryCustomerStatistics("Norway"),
                TotalAccountsInNorway = _bankStatisticsService.GetCountryAccountStatistics("Norway"),
                TotalBalanceInNorway = _bankStatisticsService.GetTotalBalanceByCountry("Norway"),

                TotalCustomersInFinland = _bankStatisticsService.GetCountryCustomerStatistics("Finland"),
                TotalAccountsInFinland = _bankStatisticsService.GetCountryAccountStatistics("Finland"),
                TotalBalanceInFinland = _bankStatisticsService.GetTotalBalanceByCountry("Finland")
            };

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CountryStatistics(string country)
        {
            if (_userService.IsUserLoggedIn() == false) return RedirectToAction("Login", "User");

            var customers = _bankStatisticsService.GetTop10CustomersByCountry(country);

            var model = new CountryStatisticsViewModel
            {
                Country = country,
                Customers = customers.ToList(),
            };

            return View(model);
        }
    }
}