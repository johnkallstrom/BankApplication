using System.Collections.Generic;
using System.Linq;

namespace Bank.Web.ViewModels
{
    public class CustomerListViewModel
    {
        public string SearchQuery { get; set; }

        public string NameSortParam { get; set; }

        public string AddressSortParam { get; set; }

        public string CountrySortParam { get; set; }

        public string CitySortParam { get; set; }

        public IEnumerable<CustomerViewModel> Customers { get; set; }

        public bool DisplaySearchError() => Customers.Count() == 0 ? true : false;
    }
}
