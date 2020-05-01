using Bank.Infrastructure.Entities;
using System.Collections.Generic;

namespace Bank.Web.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customers> GetAllCustomers();
        Customers GetCustomer(int id);
    }
}
