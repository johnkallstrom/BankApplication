using Bank.Infrastructure.Entities;
using Bank.Application.Repositories;
using System.Linq;

namespace Bank.Application.Services
{
    public class BankStatisticsService : IBankStatisticsService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;

        public BankStatisticsService(
            IAccountRepository accountRepository, 
            ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public IQueryable<Customers> GetTop10CustomersByCountry(string country) => _customerRepository.GetTop10ByCountry(country);

        public IQueryable<Accounts> GetTop10AccountsByCountry(string country) => _accountRepository.GetTop10ByCountry(country);

        public int GetCountryAccountStatistics(string country) => _accountRepository.GetAllByCountry(country).Count();

        public int GetCountryCustomerStatistics(string country) => _customerRepository.GetAllByCountry(country).Count();

        public decimal GetTotalBalanceByCountry(string country) => _accountRepository.GetAllByCountry(country).Sum(x => x.Balance);
    }
}
