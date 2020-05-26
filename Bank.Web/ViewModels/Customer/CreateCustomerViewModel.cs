using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels
{
    public class CreateCustomerViewModel
    {
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
        public List<SelectListItem> CountryOptions
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem("Choose country", $"{string.Empty}", true, true),
                    new SelectListItem("Sweden", "Sweden"),
                    new SelectListItem("Denmark", "Denmark"),
                    new SelectListItem("Norway", "Norway"),
                    new SelectListItem("Finland", "Finland"),
                };
            }
        }

        [Required(ErrorMessage = "Please enter birthday.")]
        [DisplayName("Birthday")]
        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [DisplayName("Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter email.")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
