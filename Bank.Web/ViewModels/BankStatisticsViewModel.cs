namespace Bank.Web.ViewModels
{
    public class BankStatisticsViewModel
    {
        public int TotalCustomersAmount { get; set; }
        public int TotalAccountsAmount { get; set; }
        public decimal TotalBalanceAmount { get; set; }
        public string DisplayTotalBalance => TotalBalanceAmount.ToString("C2");
    }
}
