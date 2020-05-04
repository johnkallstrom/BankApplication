using System;
using System.Collections.Generic;

namespace Bank.Web.ViewModels
{
    public class AccountDetailsViewModel
    {
        public int AccountId { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }

        public string DisplayCreated => Created.ToShortDateString();
        public string DisplayBalance => Balance.ToString("C2");
    }
}
