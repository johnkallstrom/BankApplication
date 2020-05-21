using Bank.Infrastructure.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Application.Repositories
{
    public interface ICustomerRepository
    {
        IQueryable<Customers> GetTop10ByCountry(string country);
        IQueryable<Customers> GetAllByCountry(string country);
        Task<bool> Update(Customers customer);
        Task<bool> Create(Customers customer);
        Customers Get(int id);
        IQueryable<Customers> GetAll();
    }
}
