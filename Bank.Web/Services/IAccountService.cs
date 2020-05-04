using Bank.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Web.Services
{
    public interface IAccountService
    {
        IQueryable<Transactions> GetAccountTransactions(int id);
        Accounts GetAccount(int id);
    }
}
