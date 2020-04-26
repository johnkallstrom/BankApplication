using Microsoft.AspNetCore.Mvc;

namespace Bank.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}