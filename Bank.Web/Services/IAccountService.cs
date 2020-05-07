using Bank.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Services
{
    public interface IAccountService
    {
        IQueryable<Transactions> GetAccountTransactions(int id);
        Accounts GetAccount(int id);
        Task<bool> Deposit(int id, decimal amount);
        Task<bool> Withdrawal(int id, decimal amount);
        Task<bool> Transfer(int fromAccountId, int toAccountId, decimal amount);
    }
}
