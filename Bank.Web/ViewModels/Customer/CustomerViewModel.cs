﻿namespace Bank.Web.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string NationalId { get; set; }
        public string Fullname => $"{FirstName} {LastName}";
    }
}
