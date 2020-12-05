using Bank.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Application.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customers> GetAllByID(IEnumerable<int> ids);
        IQueryable<Customers> GetTopByCountry(string country);
        IQueryable<Customers> GetAllByCountry(string country);
        Task<bool> Update(Customers customer);
        Task<bool> Create(Customers customer);
        Customers Get(int id);
        IEnumerable<Customers> GetAll();
        IEnumerable<Customers> GetAll(string sortOrder, string searchQuery);
        IEnumerable<Customers> GetAll(string searchQuery, int currentPage, int pageSize);
    }
}
