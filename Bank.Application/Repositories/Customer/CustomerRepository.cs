﻿using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Application.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
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

        public IQueryable<Customers> GetAll() => _context.Customers;
    }
}
