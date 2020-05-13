using Bank.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Web.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> Update(Accounts account);
        Task<bool> UpdateMultiple(Accounts firstAccount, Accounts secondAccount);
        Accounts Get(int id);
        IEnumerable<Accounts> GetAll();
        IEnumerable<Accounts> GetAllCustomerAccounts(int id);
    }
}
