using Microsoft.Azure.Search;
using System;

namespace Bank.Search
{
    public class Customer
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string CustomerId { get; set; }

        public string Gender { get; set; }

        [IsSearchable]
        public string Givenname { get; set; }

        [IsSearchable]
        public string Surname { get; set; }

        public string Streetaddress { get; set; }

        [IsSearchable]
        public string City { get; set; }

        public string Zipcode { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public DateTime? Birthday { get; set; }

        public string NationalId { get; set; }

        public string Telephonecountrycode { get; set; }

        public string Telephonenumber { get; set; }

        public string Emailaddress { get; set; }
    }
}
