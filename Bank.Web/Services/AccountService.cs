using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
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
    }
}
