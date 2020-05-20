using Bank.Infrastructure.Entities;
using Bank.Web.ViewModels;
using System;
using System.Linq;

namespace Bank.Web.Services.Account
{
    public class AccountDetailsViewModelBuilder
    {
        public int AccountId { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        public int StartPosition { get; set; }
        public IQueryable<TransactionViewModel> Transactions { get; set; }

        public AccountDetailsViewModelBuilder WithAccount(Accounts account)
        {
            AccountId = account.AccountId;
            Created = account.Created;
            Balance = account.Balance;

            return this;
        }

        public AccountDetailsViewModelBuilder WithTransactions(IQueryable<Transactions> transactions, int? startPosition)
        {
            StartPosition = startPosition.HasValue ? startPosition.Value : 0;

            Transactions = transactions.Select(x => new TransactionViewModel
            {
                TransactionId = x.TransactionId,
                AccountId = x.AccountId,
                Amount = x.Amount,
                Balance = x.Balance,
                Bank = x.Bank,
                Date = x.Date,
                Operation = x.Operation,
                Type = x.Type
            }).Skip(StartPosition).Take(20);

            return this;
        }

        public AccountDetailsViewModel Build()
        {
            var model = new AccountDetailsViewModel(
                AccountId,
                Created,
                Balance,
                StartPosition,
                Transactions.ToList());

            return model;
        }
    }
}
