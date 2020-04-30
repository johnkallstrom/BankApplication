using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Web.ViewModels
{
    public class CustomerProfileViewModel
    {
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string PhoneCountryCode { get; set; }
        public string Email { get; set; }
        public decimal TotalBalance => Accounts.Sum(a => a.Balance);

        public IEnumerable<AccountViewModel> Accounts { get; set; }
    }
}
