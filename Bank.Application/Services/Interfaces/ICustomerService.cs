using Bank.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customers> GetTopCustomersByCountry(string country);
        Task<bool> EditCustomer(Customers customer);
        Task<bool> CreateCustomer(Customers customer);
        IEnumerable<Customers> GetAllCustomers();
        IEnumerable<Customers> GetAllCustomers(string sortOrder, string currentFilter, string searchQuery);
        Customers GetCustomer(int id);
        IEnumerable<Accounts> GetCustomerAccounts(int id);
        Customers GetCustomerBySearch(string searchQuery);
    }
}
