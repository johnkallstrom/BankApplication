using System.Collections.Generic;

namespace Bank.Web.ViewModels
{
    public class TopCustomerListViewModel
    {
        public string Country { get; set; }
        public IEnumerable<CustomerViewModel> Customers { get; set; }
    }
}
