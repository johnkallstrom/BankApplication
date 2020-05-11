using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bank.Web.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Customers Get(int id)
        {
            return _context.Customers
                .Include(x => x.Dispositions)
                .ThenInclude(x => x.Account)
                .FirstOrDefault(x => x.CustomerId == id);
        }

        public IQueryable<Customers> GetAll() => _context.Customers;
    }
}
