using Bank.Application.Exceptions;
using Bank.Application.Repositories;
using Bank.Application.Repositories.Interfaces;
using Bank.Application.Services.Interfaces;
using Bank.Infrastructure.Entities;
using Bank.Infrastructure.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountService(
            IAccountRepository accountRepository, 
            ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }


        public Accounts GetAccount(int id) => _accountRepository.Get(id);

        public IQueryable<Transactions> GetAccountTransactions(int id, int? startPosition)
        {
            var startPos = startPosition.HasValue ? startPosition.Value : 0;
            return _transactionRepository.GetAll(id).Skip(startPos).Take(20);
        }

        public async Task<bool> Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            var fromAccount = _accountRepository.Get(fromAccountId);
            var toAccount = _accountRepository.Get(toAccountId);

            if (fromAccount == null) throw new AccountNotFoundException($"The account number '{fromAccountId}' could not be found.");
            if (toAccount == null) throw new AccountNotFoundException($"The account number '{toAccountId}' could not be found.");
            if (fromAccountId == toAccountId) throw new MatchingAccountsException("The account you try to transfer from can't be the same account you deposit to.");
            if (fromAccount.Balance - amount < 0) throw new InsufficientFundsException("Insufficient funds. The account you tried to transfer from does not have enough funds.");
            if (amount <= 0) throw new NoNegativeAmountException("No negative amount is allowed.");

            fromAccount.Balance -= Math.Round(amount, 2);
            toAccount.Balance += Math.Round(amount, 2);

            var fromTransaction = new Transactions
            {
                AccountId = fromAccount.AccountId,
                Amount = -amount,
                Balance = fromAccount.Balance,
                Date = DateTime.Now,
                Type = TransactionType.Debit.ToString(),
                Operation = OperationType.RemittanceToAnotherBank.Value
            };

            var toTransaction = new Transactions
            {
                AccountId = toAccount.AccountId,
                Amount = amount,
                Balance = toAccount.Balance,
                Date = DateTime.Now,
                Type = TransactionType.Credit.ToString(),
                Operation = OperationType.CollectionFromAnotherBank.Value
            };

            await _transactionRepository.CreateMultiple(fromTransaction, toTransaction);
            await _accountRepository.UpdateMultiple(fromAccount, toAccount);
            return true;
        }

        public async Task<bool> Withdrawal(int id, decimal amount)
        {
            var account = _accountRepository.Get(id);

            if (account == null) throw new AccountNotFoundException($"The account number '{id}' could not be found.");
            if (account.Balance - amount < 0) throw new InsufficientFundsException("Insufficient funds. The account you tried to withdraw from does not have enough funds.");
            if (amount <= 0) throw new NoNegativeAmountException("No negative amount is allowed.");

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

            await _transactionRepository.Create(transaction);
            await _accountRepository.Update(account);
            return true;
        }

        public async Task<bool> Deposit(int id, decimal amount)
        {
            var account = _accountRepository.Get(id);

            if (account == null) throw new AccountNotFoundException($"The account number '{id}' could not be found.");
            if (amount <= 0) throw new NoNegativeAmountException("No negative amount is allowed.");

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
            
            await _transactionRepository.Create(transaction);
            await _accountRepository.Update(account);
            return true;
        }
    }
}
