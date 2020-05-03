using Bank.Infrastructure.Entities;
using Bank.Web.ViewModels;
using System.Linq;

namespace Bank.Web.Services
{
    public interface ICustomerListViewModelBuilder
    {
        CustomerListViewModelBuilder WithCustomers(IQueryable<Customers> customers);
        CustomerListViewModelBuilder WithSearch(string searchWord);
        CustomerListViewModelBuilder WithPaging(int? currentPage);
        CustomerListViewModel Build();
    }
}
