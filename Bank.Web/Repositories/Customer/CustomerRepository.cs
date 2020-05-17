using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using Bank.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<CustomerViewModel> GetTop10ByCountry(string country)
        {
            var customers = _context.Dispositions
                .Include(c => c.Customer)
                .Include(a => a.Account)
                .Where(x => x.Customer.Country == country)
                .OrderByDescending(a => a.Account.Balance)
                .Select(c => new CustomerViewModel 
                {
                    CustomerId = c.CustomerId,
                    FirstName = c.Customer.Givenname,
                    LastName = c.Customer.Surname,
                    Address = c.Customer.Streetaddress,
                    City = c.Customer.City,
                    Country = c.Customer.Country,
                    TotalAccountBalance = c.Account.Balance
                })
                .Take(10);

            return customers;
        }

        public IQueryable<Customers> GetAllByCountry(string country) => _context.Customers.Where(c => c.Country == country);

        public async Task<bool> Update(Customers customer)
        {
            if (customer == null) return false;
            
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Create(Customers customer)
        {
            if (customer == null) return false;

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return true;
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
