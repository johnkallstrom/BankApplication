﻿using Bank.Infrastructure.Entities;
using System.Linq;

namespace Bank.Application.Services
{
    public interface IBankStatisticsService
    {
        IQueryable<Accounts> GetTop10AccountsByCountry(string country);
        IQueryable<Customers> GetTop10CustomersByCountry(string country);
        int GetCountryCustomerStatistics(string country);
        int GetCountryAccountStatistics(string country);
        decimal GetTotalBalanceByCountry(string country);
    }
}