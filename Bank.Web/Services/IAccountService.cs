using Bank.Infrastructure.Entities;
using System.Collections.Generic;

namespace Bank.Web.Services
{
    public interface IAccountService
    {
        IEnumerable<Accounts> GetCustomerAccounts(int id);
    }
}
