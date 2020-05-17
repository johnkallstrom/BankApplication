using Bank.Infrastructure.Entities;
using Bank.Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Repositories
{
    public interface ICustomerRepository
    {
        IQueryable<CustomerViewModel> GetTop10ByCountry(string country);
        IQueryable<Customers> GetAllByCountry(string country);
        Task<bool> Update(Customers customer);
        Task<bool> Create(Customers customer);
        Customers Get(int id);
        IQueryable<Customers> GetAll();
    }
}
