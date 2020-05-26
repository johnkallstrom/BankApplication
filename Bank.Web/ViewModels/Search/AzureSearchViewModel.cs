using System.Collections.Generic;
using System.Linq;

namespace Bank.Web.ViewModels
{
    public class AzureSearchViewModel
    {
        public string SearchString { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<CustomerViewModel> Customers { get; set; }

        public AzureSearchViewModel()
        {
            Customers = new List<CustomerViewModel>();
        }

        public bool DisplayPrevButton() => CurrentPage > 1 ? true : false;
        public bool DisplayNextButton() => CurrentPage < TotalPages ? true : false;
        public bool DisplaySearchError() => Customers.Count() == 0 ? true : false;
    }
}
