using System;
using System.Collections.Generic;

namespace Bank.Web.ViewModels
{
    public class AccountDetailsViewModel
    {
        public int AccountId { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }

        public int StartPosition { get; set; }
        public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();

        public AccountDetailsViewModel(
            int accountId, 
            DateTime created, 
            decimal balance, 
            int startPosition, 
            List<TransactionViewModel> transactions)
        {
            AccountId = accountId;
            Created = created;
            Balance = balance;
            StartPosition = startPosition;
            Transactions = transactions;
        }

        public string DisplayCreated => Created.ToShortDateString();
        public string DisplayBalance => Balance.ToString("C2");
        public bool DisplayLoadButton()
        {
            return true;
        }
    }
}
