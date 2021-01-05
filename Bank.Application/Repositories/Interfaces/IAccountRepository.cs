using Bank.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Application.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> Create(Accounts account);
        Task<bool> Update(Accounts account);
        Task<bool> UpdateMultiple(Accounts firstAccount, Accounts secondAccount);
        Accounts Get(int id);
        IEnumerable<Accounts> GetAll();
        IEnumerable<Accounts> GetAllWithCustomers();
        IEnumerable<Accounts> GetAllCustomerAccounts(int id);
        IQueryable<Accounts> GetAllByCountry(string country);
        IQueryable<Accounts> GetTop10ByCountry(string country);
    }
}
