namespace Bank.Web.ViewModels
{
    public class BankStatisticsViewModel
    {
        public int TotalCustomersInSweden { get; set; }
        public int TotalCustomersInNorway { get; set; }
        public int TotalCustomersInDenmark { get; set; }
        public int TotalCustomersInFinland { get; set; }

        public int TotalAccountsInSweden { get; set; }
        public int TotalAccountsInNorway { get; set; }
        public int TotalAccountsInDenmark { get; set; }
        public int TotalAccountsInFinland { get; set; }

        public decimal TotalBalanceInSweden { get; set; }
        public decimal TotalBalanceInNorway { get; set; }
        public decimal TotalBalanceInDenmark { get; set; }
        public decimal TotalBalanceInFinland { get; set; }
    }
}
