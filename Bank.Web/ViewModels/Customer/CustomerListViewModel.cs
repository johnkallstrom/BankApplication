using System.Collections.Generic;
using System.Linq;

namespace Bank.Web.ViewModels
{
    public class CustomerListViewModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string SearchWord { get; set; }
        public List<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();

        public CustomerListViewModel(
            int currentPage, 
            int pageSize, 
            int totalPages,
            string searchWord,
            List<CustomerViewModel> customers)
        {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.TotalPages = totalPages;
            this.SearchWord = searchWord;
            this.Customers = customers;
        }

        public bool DisplayPrevButton() => CurrentPage > 1 ? true : false;
        public bool DisplayNextButton() => CurrentPage < TotalPages ? true : false;
        public bool DisplaySearchError() => Customers.Count() == 0 ? true : false;
    }
}
