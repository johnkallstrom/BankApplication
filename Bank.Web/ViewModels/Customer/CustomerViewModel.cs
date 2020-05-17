namespace Bank.Web.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public decimal TotalAccountBalance { get; set; }
        public string DisplayTotalBalance => TotalAccountBalance.ToString("C");
        public string Fullname => $"{FirstName} {LastName}";
    }
}
