using Bank.Infrastructure.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Repositories
{
    public interface ITransactionRepository
    {
        IQueryable<Transactions> GetAll(int id);
        Task<bool> Create(Transactions transaction);
        Task<bool> CreateMultiple(Transactions firstTransaction, Transactions secondTransaction);
    }
}
