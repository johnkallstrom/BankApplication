using Bank.Infrastructure.Entities;
using Bank.Web.ViewModels;
using System;
using System.Linq;

namespace Bank.Web.Services
{
    public class CustomerListViewModelBuilder
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string SearchWord { get; set; }
        public IQueryable<CustomerViewModel> Customers { get; set; }

        public CustomerListViewModelBuilder WithCustomers(IQueryable<Customers> customers)
        {
            Customers = customers.Select(x => new CustomerViewModel
            {
                CustomerId = x.CustomerId,
                FirstName = x.Givenname,
                LastName = x.Surname,
                Address = x.Streetaddress,
                City = x.City
            });

            return this;
        }

        public CustomerListViewModelBuilder WithSearch(string searchWord)
        {
            if (!string.IsNullOrEmpty(searchWord))
            {
                Customers = Customers
                    .Where(c => c.FirstName.Contains(searchWord) 
                    || c.LastName.Contains(searchWord) 
                    || c.City.Contains(searchWord));
            }

            return this;
        }

        public CustomerListViewModelBuilder WithPaging(int? currentPage)
        {
            PageSize = 50;
            CurrentPage = currentPage.HasValue ? currentPage.Value : 1;
            TotalPages = (int)Math.Ceiling((double)Customers.Count() / PageSize);

            Customers = Customers.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

            return this;
        }

        public CustomerListViewModel Build()
        {
            var model = new CustomerListViewModel( 
                CurrentPage, 
                PageSize, 
                TotalPages,
                SearchWord,
                Customers.ToList());

            return model;
        }

    }
}
