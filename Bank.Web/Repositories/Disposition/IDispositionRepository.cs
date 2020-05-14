using Bank.Infrastructure.Entities;
using System.Threading.Tasks;

namespace Bank.Web.Repositories.Disposition
{
    public interface IDispositionRepository
    {
        Task<bool> Create(Dispositions disposition);
    }
}
