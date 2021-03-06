﻿using Bank.Application.Repositories.Interfaces;
using Bank.Application.Services.Interfaces;
using Bank.Infrastructure.Entities;
using Bank.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDispositionRepository _dispositionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            IDispositionRepository dispositionRepository,
            IAccountRepository accountRepository, 
            ICustomerRepository customerRepository)
        {
            _dispositionRepository = dispositionRepository;
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customers> GetTopCustomersByCountry(string country) => _customerRepository.GetTopByCountry(country);

        public async Task<bool> EditCustomer(Customers customer)
        {
            if (customer == null) return false;

            await _customerRepository.Update(customer);
            return true;
        }

        public async Task<bool> CreateCustomer(Customers customer)
        {
            if (customer == null) return false;

            switch (customer.Country)
            {
                case "Sweden":
                    customer.CountryCode = CountryCodeType.SE.ToString();
                    customer.Telephonecountrycode = "46";
                    break;
                case "Denmark":
                    customer.CountryCode = CountryCodeType.DK.ToString();
                    customer.Telephonecountrycode = "45";
                    break;
                case "Norway":
                    customer.CountryCode = CountryCodeType.NO.ToString();
                    customer.Telephonecountrycode = "47";
                    break;
                case "Finland":
                    customer.CountryCode = CountryCodeType.FI.ToString();
                    customer.Telephonecountrycode = "358";
                    break;
            }

            var account = new Accounts
            {
                Balance = 0m,
                Created = DateTime.Now,
                Frequency = FrequencyType.Monthly.ToString()
            };

            await _customerRepository.Create(customer);
            await _accountRepository.Create(account);

            var disposition = new Dispositions
            {
                CustomerId = customer.CustomerId,
                AccountId = account.AccountId,
                Type = DispositionType.OWNER.ToString()
            };

            await _dispositionRepository.Create(disposition);
            return true;
        }

        public Customers GetCustomer(int id) => _customerRepository.Get(id);

        public IEnumerable<Accounts> GetCustomerAccounts(int id) => _accountRepository.GetAllCustomerAccounts(id);

        public Customers GetCustomerBySearch(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return null;
            if (int.TryParse(searchString, out int id) == false) return null;
            if (id <= 0) return null;

            return _customerRepository.Get(id);
        }

        public IEnumerable<Customers> GetAllCustomers() => _customerRepository.GetAll();

        public IEnumerable<Customers> GetAllCustomers(string sortOrder, string currentFilter, string searchQuery)
        {
            return _customerRepository.GetAll(sortOrder, currentFilter, searchQuery);
        }
    }
}
