using Bank.Infrastructure.Entities;
using System.Threading.Tasks;

namespace Bank.Application.Repositories.Interfaces
{
    public interface IDispositionRepository
    {
        Task<bool> Create(Dispositions disposition);
    }
}
