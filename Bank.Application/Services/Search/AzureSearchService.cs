using Bank.Infrastructure.Entities;
using Bank.Infrastructure.SearchModels;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Application.Services.Search
{
    public class AzureSearchService : IAzureSearchService
    {
        private readonly IConfiguration _configuration;
        private readonly ISearchIndexClient _indexClient;

        public AzureSearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            _indexClient = InitializeSearchIndex();
        }

        public async Task<DocumentSearchResult<CustomerSearch>> RunQueryAsync(string searchString, int page)
        {
            if (string.IsNullOrEmpty(searchString)) searchString = "*";

            var parameters = new SearchParameters
            {
                Select = new[] { "CustomerStringId, CustomerId, NationalId, Givenname, Surname, City, Streetaddress" },
                IncludeTotalResultCount = true,
                OrderBy = new[] { "CustomerId asc" },
                Skip = (page - 1) * 50,
                Top = 50,
            };

            var searchResults = await _indexClient.Documents.SearchAsync<CustomerSearch>(searchString, parameters);
            return searchResults;
        }

        public async Task MergeOrUploadCustomersAsync(IEnumerable<Customers> customers)
        {
            var actions = new List<IndexAction<CustomerSearch>>();
            foreach (var customer in customers)
            {
                actions.Add(new IndexAction<CustomerSearch>
                {
                    ActionType = IndexActionType.MergeOrUpload,
                    Document = new CustomerSearch
                    {
                        CustomerStringId = customer.CustomerId.ToString(),
                        CustomerId = customer.CustomerId,
                        Givenname = customer.Givenname,
                        Surname = customer.Surname,
                        Gender = customer.Gender,
                        Emailaddress = customer.Emailaddress,
                        Streetaddress = customer.Streetaddress,
                        City = customer.City,
                        Country = customer.Country,
                        CountryCode = customer.CountryCode,
                        NationalId = customer.NationalId,
                        Telephonenumber = customer.Telephonenumber,
                        Telephonecountrycode = customer.Telephonecountrycode,
                        Zipcode = customer.Zipcode
                    }
                });
            }

            var batch = IndexBatch.New(actions);
            await _indexClient.Documents.IndexAsync(batch);
        }

        private ISearchIndexClient InitializeSearchIndex()
        {
            string searchServiceName = _configuration.GetValue<string>("AzureSearch:SearchServiceName");
            string adminApiKey = _configuration.GetValue<string>("AzureSearch:SearchServiceAdminApiKey");
            string indexName = _configuration.GetValue<string>("AzureSearch:SearchIndexName");

            var serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            var indexClient = serviceClient.Indexes.GetClient(indexName);
            return indexClient;
        }
    }
}
