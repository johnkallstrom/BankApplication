using Bank.Infrastructure.Entities;
using System.Linq;

namespace Bank.Web.Repositories
{
    public interface ICustomerRepository
    {
        Customers Get(int id);
        IQueryable<Customers> GetAll();
    }
}
