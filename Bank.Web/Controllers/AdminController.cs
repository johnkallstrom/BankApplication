using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Web.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        [Authorize(Roles = ("Admin"))]
        public IActionResult Index()
        {
            return View();
        }
    }
}