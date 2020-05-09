using Bank.Web.Services;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBankStatisticsService _bankStatisticsService;

        public HomeController(IBankStatisticsService bankStatisticsService)
        {
            _bankStatisticsService = bankStatisticsService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(CacheProfileName = "Default")]
        public IActionResult Index()
        {
            var model = new BankStatisticsViewModel
            {
                TotalCustomersAmount = _bankStatisticsService.GetTotalCustomersAmount(),
                TotalAccountsAmount = _bankStatisticsService.GetTotalAccountsAmount(),
                TotalBalanceAmount = _bankStatisticsService.GetTotalBalanceAmount()
            };

            return View(model);
        }
    }
}