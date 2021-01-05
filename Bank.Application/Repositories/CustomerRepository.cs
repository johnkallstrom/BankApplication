using Bank.Application.Repositories.Interfaces;
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

        public IEnumerable<Customers> GetAll() => _context.Customers;

        public IEnumerable<Customers> GetAll(string sortOrder, string currentFilter, string searchQuery)
        {
            var collection = _context.Customers as IQueryable<Customers>;

            if (!string.IsNullOrWhiteSpace(searchQuery) || !string.IsNullOrWhiteSpace(currentFilter))
            {
                var query = !string.IsNullOrWhiteSpace(searchQuery) ? searchQuery.Trim() : currentFilter.Trim();

                collection = collection.Where(x => x.Givenname.Contains(query)
                        || x.Surname.Contains(query)
                        || x.City.Contains(query)
                        || x.Country.Contains(query));
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    collection = collection.OrderByDescending(x => x.Givenname);
                    break;
                case "address":
                    collection = collection.OrderBy(x => x.Streetaddress);
                    break;
                case "address_desc":
                    collection = collection.OrderByDescending(x => x.Streetaddress);
                    break;
                case "country":
                    collection = collection.OrderBy(x => x.Country);
                    break;
                case "country_desc":
                    collection = collection.OrderByDescending(x => x.Country);
                    break;
                case "city":
                    collection = collection.OrderBy(x => x.City);
                    break;
                case "city_desc":
                    collection = collection.OrderByDescending(x => x.City);
                    break;
                default:
                    collection = collection.OrderBy(x => x.Givenname);
                    break;
            }

            return collection;
        }

        public Customers Get(int id)
        {
            return _context.Customers
                .Include(x => x.Dispositions)
                .ThenInclude(x => x.Account)
                .FirstOrDefault(x => x.CustomerId == id);
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
    }
}
