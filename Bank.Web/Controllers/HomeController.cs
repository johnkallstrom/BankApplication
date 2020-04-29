using Bank.Web.Services;
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
            return View();
        }
    }
}