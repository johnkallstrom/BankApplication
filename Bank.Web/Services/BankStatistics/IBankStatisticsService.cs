using Bank.Infrastructure.Entities;
using Bank.Web.ViewModels;
using System.Linq;

namespace Bank.Web.Services
{
    public interface IBankStatisticsService
    {
        IQueryable<Accounts> GetTop10AccountsByCountry(string country);
        IQueryable<CustomerViewModel> GetTop10CustomersByCountry(string country);
        int GetCountryCustomerStatistics(string country);
        int GetCountryAccountStatistics(string country);
        decimal GetTotalBalanceByCountry(string country);
    }
}
