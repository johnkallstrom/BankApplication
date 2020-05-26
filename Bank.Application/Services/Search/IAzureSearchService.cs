using Bank.Infrastructure.Entities;
using Bank.Infrastructure.SearchModels;
using Microsoft.Azure.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Application.Services.Search
{
    public interface IAzureSearchService
    {
        Task<DocumentSearchResult<CustomerSearch>> RunQueryAsync(string searchString, int page);
        Task MergeOrUploadCustomersAsync(IEnumerable<Customers> customers);
    }
}
