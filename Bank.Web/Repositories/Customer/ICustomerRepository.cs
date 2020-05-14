using Bank.Infrastructure.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool> Create(Customers customer);
        Customers Get(int id);
        IQueryable<Customers> GetAll();
    }
}
