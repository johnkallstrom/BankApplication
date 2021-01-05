using Bank.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Application.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        IQueryable<Customers> GetTopByCountry(string country);
        IQueryable<Customers> GetAllByCountry(string country);
        Task<bool> Update(Customers customer);
        Task<bool> Create(Customers customer);
        Customers Get(int id);
        IEnumerable<Customers> GetAll();
        IEnumerable<Customers> GetAll(string sortOrder, string currentFilter, string searchQuery);
    }
}
