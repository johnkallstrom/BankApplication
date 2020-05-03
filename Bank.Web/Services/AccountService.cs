using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Accounts GetAccount(int id) => _context.Accounts.FirstOrDefault(a => a.AccountId == id);

        public IQueryable<Transactions> GetAccountTransactions(int id)
        {
            return _context.Transactions
                .Where(t => t.AccountId == id)
                .OrderByDescending(t => t.Date);
        }

        public IEnumerable<Accounts> GetCustomerAccounts(int id)
        {
            return _context.Dispositions
                .Include(a => a.Account)
                .Where(d => d.CustomerId == id)
                .Select(x => new Accounts
                {
                    AccountId = x.Account.AccountId,
                    Balance = x.Account.Balance,
                    Created = x.Account.Created,
                    Frequency = x.Account.Frequency,
                    Loans = x.Account.Loans,
                    Dispositions = x.Account.Dispositions,
                    PermenentOrder = x.Account.PermenentOrder,
                    Transactions = x.Account.Transactions
                }).ToList();
        }
    }
}
