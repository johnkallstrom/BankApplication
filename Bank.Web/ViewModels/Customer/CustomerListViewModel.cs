using X.PagedList;

namespace Bank.Web.ViewModels
{
    public class CustomerListViewModel
    {
        public string SearchQuery { get; set; }

        public string CurrentFilter { get; set; }

        public CustomerListSortingParameters SortingParameters { get; set; }

        public IPagedList<CustomerViewModel> Customers { get; set; }
    }

    public class CustomerListSortingParameters
    {
        public string Current { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
