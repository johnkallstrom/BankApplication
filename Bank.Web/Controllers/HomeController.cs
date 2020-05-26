using Bank.Application.Services;
using Bank.Infrastructure.Enums;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Bank.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public HomeController(
            IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(CacheProfileName = "Default")]
        public IActionResult Index()
        {
            var countries = new string[]
            {
                CountryType.Sweden.ToString(),
                CountryType.Norway.ToString(),
                CountryType.Denmark.ToString(),
                CountryType.Finland.ToString(),
            };

            var model = new BankStatisticsViewModel
            {
                TotalCustomers = _statisticsService.GetTotalCustomers(),
                TotalAccounts = _statisticsService.GetTotalAccounts(),
                TotalBalance = _statisticsService.GetTotalBalance(),

                CountryStatisticsViewModel = new CountryStatisticsViewModel
                {
                    Countries = countries.Select(country => new CountryViewModel
                    {
                        Country = country,
                        TotalCustomers = _statisticsService.GetTotalCustomersByCountry(country),
                        TotalAccounts = _statisticsService.GetTotalAccountsByCountry(country),
                        TotalBalance = _statisticsService.GetTotalBalanceByCountry(country)
                    }).ToList()
                }
            };

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ViewSearchError() => View();
    }
}