using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using System.Threading.Tasks;

namespace Bank.Application.Repositories
{
    public class DispositionRepository : IDispositionRepository
    {
        private readonly BankAppDataContext _context;

        public DispositionRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Dispositions disposition)
        {
            if (disposition == null) return false;

            await _context.Dispositions.AddAsync(disposition);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
