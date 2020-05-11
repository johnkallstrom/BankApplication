using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Transactions transaction)
        {
            if (transaction == null) return false;

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateMultiple(Transactions firstTransaction, Transactions secondTransaction)
        {
            if (firstTransaction == null) return false;
            if (secondTransaction == null) return false;

            await _context.Transactions.AddRangeAsync(firstTransaction, secondTransaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<Transactions> GetAll(int id)
        {
            return _context.Transactions
                 .Where(t => t.AccountId == id)
                 .OrderByDescending(t => t.TransactionId)
                 .ThenByDescending(t => t.Date);
        }
    }
}
