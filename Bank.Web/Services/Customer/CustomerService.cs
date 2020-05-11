using Bank.Infrastructure.Entities;
using Bank.Web.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            IAccountRepository accountRepository, 
            ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public IQueryable<Customers> GetAllCustomers() => _customerRepository.GetAll();

        public Customers GetCustomer(int id) => _customerRepository.Get(id);

        public IEnumerable<Accounts> GetCustomerAccounts(int id) => _accountRepository.GetAllCustomerAccounts(id);

        public Customers GetCustomerBySearch(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return null;
            if (int.TryParse(searchString, out int id) == false) return null;
            if (id <= 0) return null;

            return _customerRepository.Get(id);
        }
    }
}
