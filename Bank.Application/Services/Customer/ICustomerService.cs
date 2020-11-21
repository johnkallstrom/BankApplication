﻿using Bank.Infrastructure.Entities;
using Bank.Infrastructure.SearchModels;
using Microsoft.Azure.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Application.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customers> GetTopCustomersByCountry(string country);
        IEnumerable<Customers> GetCustomersByIndex(DocumentSearchResult<CustomerSearch> searchResults);
        Task<bool> EditCustomer(Customers customer);
        Task<bool> CreateCustomer(Customers customer);
        int GetAllCustomersCount();
        IEnumerable<Customers> GetAllCustomers(string searchString, int page);
        Customers GetCustomer(int id);
        IEnumerable<Accounts> GetCustomerAccounts(int id);
        Customers GetCustomerBySearch(string searchString);
    }
}
