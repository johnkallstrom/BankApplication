using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bank.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Customers GetCustomer(int id)
        {
            return _context.Customers
                .Include(x => x.Dispositions)
                .ThenInclude(x => x.Account)
                .ThenInclude(x => x.Transactions)
                .FirstOrDefault(x => x.CustomerId == id);
        }
    }
}
