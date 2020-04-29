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

        public Customers GetCustomerByID(int id)
        {
            return _context.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(c => c.Account)
                .FirstOrDefault(c => c.CustomerId == id);
        }
    }
}
