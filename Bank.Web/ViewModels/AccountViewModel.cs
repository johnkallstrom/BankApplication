using System;
using System.Collections.Generic;

namespace Bank.Web.ViewModels
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}
