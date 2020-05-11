using Bank.Infrastructure.Entities;
using System.Collections.Generic;

namespace Bank.Web.Repositories
{
    public interface IAccountRepository
    {
        Accounts Get(int id);
        IEnumerable<Accounts> GetAll();
        IEnumerable<Accounts> GetAllCustomerAccounts(int id);
    }
}
