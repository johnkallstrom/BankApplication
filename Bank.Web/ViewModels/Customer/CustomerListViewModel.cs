using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace Bank.Web.ViewModels
{
    public class CustomerListViewModel
    {
        public string SearchQuery { get; set; }

        public string CurrentFilter { get; set; }

        public string CurrentSort { get; set; }

        public string NameSortParam { get; set; }

        public string AddressSortParam { get; set; }

        public string CountrySortParam { get; set; }

        public string CitySortParam { get; set; }

        public IPagedList<CustomerViewModel> Customers { get; set; }
    }
}
