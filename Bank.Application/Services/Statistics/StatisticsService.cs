using Bank.Application.Repositories;
using System.Linq;

namespace Bank.Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;

        public StatisticsService(
            IAccountRepository accountRepository,
            ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public int GetTotalCustomersByCountry(string country) => _customerRepository.GetAllByCountry(country).Count();

        public int GetTotalAccountsByCountry(string country) => _accountRepository.GetAllByCountry(country).Count();

        public decimal GetTotalBalanceByCountry(string country) => _accountRepository.GetAllByCountry(country).Sum(x => x.Balance);

        public int GetTotalCustomers() => _customerRepository.GetAll().Count();

        public int GetTotalAccounts() => _accountRepository.GetAll().Count();

        public decimal GetTotalBalance() => _accountRepository.GetAll().Sum(x => x.Balance);
    }
}
