using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Web.ViewComponents.TransactionList
{
    public class TransactionList : ViewComponent
    {
        public IViewComponentResult Invoke(AccountDetailsViewModel model)
        {
            return View("TransactionList", model);
        }
    }
}
