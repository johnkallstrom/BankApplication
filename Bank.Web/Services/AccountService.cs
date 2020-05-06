using Bank.Infrastructure;
using Bank.Infrastructure.Entities;
using Bank.Infrastructure.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Withdrawal(int id, decimal amount)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.AccountId == id);

            if (account == null) return false;
            if (account.Balance - amount < 0) return false;

            account.Balance -= Math.Round(amount, 2);

            var transaction = new Transactions
            {
                AccountId = account.AccountId,
                Amount = -amount,
                Balance = account.Balance,
                Date = DateTime.Now,
                Type = TransactionType.Debit.ToString(),
                Operation = OperationType.WithdrawalInCash.Value
            };

            _context.Transactions.Add(transaction);
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Deposit(int id, decimal amount)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.AccountId == id);
            if (account == null) return false;

            account.Balance += Math.Round(amount, 2);

            var transaction = new Transactions
            {
                AccountId = account.AccountId,
                Amount = amount,
                Balance = account.Balance,
                Date = DateTime.Now,
                Type = TransactionType.Credit.ToString(),
                Operation = OperationType.CreditInCash.Value
            };
             
            _context.Transactions.Add(transaction);
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public Accounts GetAccount(int id) => _context.Accounts.FirstOrDefault(a => a.AccountId == id);

        public IQueryable<Transactions> GetAccountTransactions(int id)
        {
            return _context.Transactions
                .Where(t => t.AccountId == id)
                .OrderByDescending(t => t.TransactionId)
                .ThenByDescending(t => t.Date);
        }
    }
}
