namespace Bank.Application.Services.Interfaces
{
    public interface IStatisticsService
    {
        int GetTotalCustomers();
        int GetTotalAccounts();
        decimal GetTotalBalance();
        int GetTotalCustomersByCountry(string country);
        int GetTotalAccountsByCountry(string country);
        decimal GetTotalBalanceByCountry(string country);
    }
}
