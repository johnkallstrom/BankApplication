using Bank.Infrastructure.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Application.Repositories
{
    public interface ITransactionRepository
    {
        Transactions GetByAccountId(int id);
        IQueryable<Transactions> GetAll(int id);
        Task<bool> Create(Transactions transaction);
        Task<bool> CreateMultiple(Transactions firstTransaction, Transactions secondTransaction);
    }
}
