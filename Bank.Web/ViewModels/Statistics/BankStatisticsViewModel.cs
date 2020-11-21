namespace Bank.Web.ViewModels
{
    public class BankStatisticsViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalBalance { get; set; }

        public CountryStatisticsViewModel CountryStatisticsViewModel { get; set; }
        public string DisplayTotalBalance => TotalBalance.ToString("C3");
    }
}
