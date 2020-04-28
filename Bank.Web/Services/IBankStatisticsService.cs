namespace Bank.Web.Services
{
    public interface IBankStatisticsService
    {
        int GetTotalCustomersAmount();
        int GetTotalAccountsAmount();
        decimal GetTotalBalanceAmount();
    }
}
