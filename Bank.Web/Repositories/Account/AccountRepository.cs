using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Web.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Accounts Get(int id) => _context.Accounts.FirstOrDefault(a => a.AccountId == id);

        public IEnumerable<Accounts> GetAll() => _context.Accounts;

        public IEnumerable<Accounts> GetAllCustomerAccounts(int id)
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
