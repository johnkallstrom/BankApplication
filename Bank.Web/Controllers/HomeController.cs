using Bank.Web.Services;
using Bank.Web.ViewModels;
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