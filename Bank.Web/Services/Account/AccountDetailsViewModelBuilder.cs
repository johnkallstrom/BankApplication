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

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public IQueryable<TransactionViewModel> Transactions { get; set; }

        public AccountDetailsViewModelBuilder WithAccount(Accounts account)
        {
            AccountId = account.AccountId;
            Created = account.Created;
            Balance = account.Balance;

            return this;
        }

        public AccountDetailsViewModelBuilder WithTransactions(IQueryable<Transactions> transactions)
        {
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
            });

            return this;
        }

        public AccountDetailsViewModelBuilder WithPaging(int? currentPage)
        {
            PageSize = 20;
            CurrentPage = currentPage.HasValue ? currentPage.Value : 1;
            TotalPages = (int)Math.Ceiling((double)Transactions.Count() / PageSize);
            Transactions = Transactions.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

            return this;
        }

        public AccountDetailsViewModel Build()
        {
            var model = new AccountDetailsViewModel(
                AccountId,
                Created,
                Balance,
                CurrentPage,
                PageSize,
                TotalPages,
                Transactions.ToList());

            return model;
        }
    }
}
