using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using System.Threading.Tasks;

namespace Bank.Web.Repositories.Disposition
{
    public class DispositionRepository : IDispositionRepository
    {
        private readonly ApplicationDbContext _context;

        public DispositionRepository(ApplicationDbContext context)
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
