using Bank.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Web.Services
{
    public interface ICustomerService
    {
        IQueryable<Customers> GetAllCustomers();
        Customers GetCustomer(int id);
        IEnumerable<Accounts> GetCustomerAccounts(int id);
        Customers GetCustomerBySearch(string searchString);
    }
}
