using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Accounts> GetTop10ByCountry(string country)
        {
            var accounts = _context.Dispositions
                  .Include(c => c.Customer)
                  .Include(a => a.Account)
                  .Where(x => x.Customer.Country == country)
                  .OrderByDescending(a => a.Account.Balance)
                  .Select(c => c.Account)
                  .Take(10);

            return accounts;
        }

        public IQueryable<Accounts> GetAllByCountry(string country)
        {
            return _context.Dispositions
                 .Include(a => a.Account)
                 .Where(d => d.Customer.Country == country)
                 .Select(x => x.Account);
        }

        public async Task<bool> Create(Accounts account)
        {
            if (account == null) return false;

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMultiple(Accounts firstAccount, Accounts secondAccount)
        {
            _context.Accounts.UpdateRange(firstAccount, secondAccount);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Accounts account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return true;
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
