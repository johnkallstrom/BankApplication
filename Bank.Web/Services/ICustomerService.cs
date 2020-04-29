using Bank.Infrastructure.Entities;

namespace Bank.Web.Services
{
    public interface ICustomerService
    {
        Customers GetCustomerByID(int id);
    }
}
