using Bank.Application.Services;
using Bank.Infrastructure.SearchModels;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Bank.Search
{
    public class App
    {
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _config;

        public App(
            ICustomerService customerService,
            IConfiguration config)
        {
            _customerService = customerService;
            _config = config;
        }

        //public void Run()
        //{
        //    string searchServiceName = _config["SearchServiceName"];
        //    string adminApiKey = _config["SearchServiceAdminApiKey"];
        //    string indexName = _config["SearchIndexName"];

        //    var serviceClient = CreateSearchServiceClient(searchServiceName, adminApiKey);
        //    var indexClient = serviceClient.Indexes.GetClient(indexName);

        //    DeleteIndexIfExists(serviceClient, indexName);
        //    CreateIndex(serviceClient, indexName);
        //    UploadDocuments(indexClient);
        //}

        //private void UploadDocuments(ISearchIndexClient indexClient)
        //{
        //    var customers = _customerService.GetAllCustomers();
        //    var actions = new List<IndexAction<CustomerSearch>>();

        //    Console.WriteLine("Uploading documents...");
        //    foreach (var customer in customers)
        //    {
        //        actions.Add(new IndexAction<CustomerSearch>
        //        {
        //            ActionType = IndexActionType.Upload,
        //            Document = new CustomerSearch
        //            {
        //                CustomerStringId = customer.CustomerId.ToString(),
        //                CustomerId = customer.CustomerId,
        //                Givenname = customer.Givenname,
        //                Surname = customer.Surname,
        //                Gender = customer.Gender,
        //                Emailaddress = customer.Emailaddress,
        //                Streetaddress = customer.Streetaddress,
        //                City = customer.City,
        //                Country = customer.Country,
        //                CountryCode = customer.CountryCode,
        //                NationalId = customer.NationalId,
        //                Telephonenumber = customer.Telephonenumber,
        //                Telephonecountrycode = customer.Telephonecountrycode,
        //                Zipcode = customer.Zipcode
        //            }
        //        });
        //    }

        //    try
        //    {
        //        var batch = IndexBatch.New(actions);
        //        indexClient.Documents.Index(batch);
        //    }
        //    catch (IndexBatchException e)
        //    {
        //        Console.WriteLine(
        //            "Failed to index some of the documents: {0}",
        //            String.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
        //    }

        //    Console.WriteLine("Waiting for indexing...");
        //    Thread.Sleep(2000);
        //}

        private SearchServiceClient CreateSearchServiceClient(string searchServiceName, string adminApiKey)
        {
            var serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            return serviceClient;
        }

        private void DeleteIndexIfExists(SearchServiceClient serviceClient, string indexName)
        {
            if (serviceClient.Indexes.Exists(indexName))
            {
                Console.WriteLine("Deleting index...");
                serviceClient.Indexes.Delete(indexName);
            }
        }

        private void CreateIndex(SearchServiceClient serviceClient, string indexName)
        {
            var definition = new Microsoft.Azure.Search.Models.Index
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<CustomerSearch>()
            };

            Console.WriteLine("Creating index...");
            serviceClient.Indexes.Create(definition);
        }
    }
}
