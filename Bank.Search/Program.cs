using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Bank.Search
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
            var configuration = builder.Build();
            var serviceClient = CreateSearchServiceClient(configuration);

            string indexName = configuration["SearchIndexName"];

            if (serviceClient.Indexes.Exists(indexName))
            {
                Console.WriteLine("Deleting index...");
                serviceClient.Indexes.Delete(indexName);
            }
            else
            {
                Console.WriteLine("Creating index...");
                CreateIndex(indexName, serviceClient);
            }

            var actions = new List<IndexAction<Customer>>();
        }

        private static void CreateIndex(string indexName, SearchServiceClient serviceClient)
        {
            var definition = new Microsoft.Azure.Search.Models.Index
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<Customer>()
            };

            serviceClient.Indexes.Create(definition);
        }

        private static SearchServiceClient CreateSearchServiceClient(IConfigurationRoot configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string adminApiKey = configuration["SearchServiceAdminApiKey"];

            var serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            return serviceClient;
        }
    }
}
