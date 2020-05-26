namespace Bank.Web.ViewModels
{
    public class CountryViewModel
    {
        public string Country { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalBalance { get; set; }

        public string DisplayTotalBalance => TotalBalance.ToString("C2");
    }
}
