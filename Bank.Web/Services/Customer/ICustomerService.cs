﻿using Bank.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Services
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomer();
        IQueryable<Customers> GetAllCustomers();
        Customers GetCustomer(int id);
        IEnumerable<Accounts> GetCustomerAccounts(int id);
        Customers GetCustomerBySearch(string searchString);
    }
}