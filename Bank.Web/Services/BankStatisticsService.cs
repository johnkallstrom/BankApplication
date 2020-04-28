using Bank.Infrastructure;
using System.Linq;

namespace Bank.Web.Services
{
    public class BankStatisticsService : IBankStatisticsService
    {
        private readonly ApplicationDbContext _context;

        public BankStatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public int GetTotalAccountsAmount()
        {
            return _context.Accounts.Count();
        }

        public decimal GetTotalBalanceAmount()
        {
            return _context.Accounts.Sum(a => a.Balance);
        }

        public int GetTotalCustomersAmount()
        {
            return _context.Customers.Count();
        }
    }
}
