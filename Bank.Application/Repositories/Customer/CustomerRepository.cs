using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Application.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankAppDataContext _context;

        public CustomerRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Customers> GetAllByID(IEnumerable<int> ids)
        {
            var customers = new List<Customers>();

            foreach (var customer in _context.Customers)
            {
                if (ids.Contains(customer.CustomerId))
                {
                    customers.Add(customer);
                }
            }

            return customers;
        }

        public IQueryable<Customers> GetTopByCountry(string country)
        {
            var customers = _context.Dispositions
                .Include(c => c.Customer)
                .Include(a => a.Account)
                .Where(x => x.Customer.Country == country)
                .OrderByDescending(a => a.Account.Balance)
                .Select(c => c.Customer)
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

        public IQueryable<Customers> GetAll()
        {
            return _context.Customers;
        }

        public IEnumerable<Customers> GetAll(string searchString, int page)
        {
            int take = 50;
            int skip = (page - 1) * take;

            if (string.IsNullOrWhiteSpace(searchString))
            {
                return _context.Customers.Skip(skip).Take(50).ToList();
            }

            var collection = _context.Customers as IQueryable<Customers>;

            if (string.IsNullOrWhiteSpace(searchString) == false)
            {
                var query = searchString.Trim();

                collection = collection
                        .Where(x => x.Givenname.Contains(query)
                        || x.Surname.Contains(query)
                        || x.City.Contains(query)
                        || x.Country.Contains(query)
                        || x.Streetaddress.Contains(query));
            }

            return collection.ToList();
        }
    }
}
