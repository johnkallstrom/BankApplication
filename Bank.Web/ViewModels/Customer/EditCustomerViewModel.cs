using Bank.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels
{
    public class EditCustomerViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter first name.")]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter gender.")]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter city.")]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter address.")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter postal code.")]
        [DisplayName("Postal code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Please enter country.")]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required]
        public string CountryCode { get; set; }

        [Required]
        public List<SelectListItem> CountryOptions
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem("Sweden", "Sweden"),
                    new SelectListItem("Denmark", "Denmark"),
                    new SelectListItem("Norway", "Norway"),
                    new SelectListItem("Finland", "Finland"),
                };
            }
        }

        [Required(ErrorMessage = "Please enter social security number.")]
        [DisplayName("Social security number (Ex 920505-0145)")]
        [RegularExpression
            ("^(?:19|[2-9][0-9]){0,1}(?:[0-9]{2})" +
            "(?!0229|0230|0231|0431|0631|0931|1131)" +
            "(?:(?:0[1-9])|(?:1[0-2]))(?:(?:0[1-9])" +
            "|(?:1[0-9])|(?:2[0-9])|(?:3[01]))[-+](?!0000)" +
            "(?:[0-9]{4})$",
            ErrorMessage = "Please enter a correct social security number.")]
        public string SocialSecurityNumber { get; set; }

        [Required(ErrorMessage = "Please enter birthday.")]
        [DisplayName("Birthday")]
        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [DisplayName("Phone")]
        public string Phone { get; set; }

        [Required]
        public string PhoneCountryCode { get; set; }

        [Required(ErrorMessage = "Please enter email.")]
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
