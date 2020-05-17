using System.Collections.Generic;

namespace Bank.Web.ViewModels
{
    public class CountryStatisticsViewModel
    {
        public string Country { get; set; }
        public IEnumerable<CustomerViewModel> Customers { get; set; }
    }
}
