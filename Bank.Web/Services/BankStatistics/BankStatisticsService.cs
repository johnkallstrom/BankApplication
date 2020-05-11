using Bank.Web.Repositories;
using System.Linq;

namespace Bank.Web.Services
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

        public int GetTotalAccountsAmount() => _accountRepository.GetAll().Count();

        public decimal GetTotalBalanceAmount() => _accountRepository.GetAll().Sum(x => x.Balance);

        public int GetTotalCustomersAmount() => _customerRepository.GetAll().Count();
    }
}
