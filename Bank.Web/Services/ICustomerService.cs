using Bank.Infrastructure.Entities;
using System.Linq;

namespace Bank.Web.Services
{
    public interface ICustomerService
    {
        IQueryable<Customers> GetAllCustomers();
        Customers GetCustomer(int id);
    }
}
