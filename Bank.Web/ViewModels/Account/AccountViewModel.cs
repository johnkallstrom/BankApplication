    using System;

namespace Bank.Web.ViewModels
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        public string DisplayCreated => Created.ToShortDateString();
        public string DisplayBalance => Balance.ToString("C2");
    }
}
