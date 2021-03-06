﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please enter email.")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Current role")]
        public string CurrentRole { get; set; }

        [Required(ErrorMessage = "Please choose a role.")]
        [DisplayName("New role")]
        public string NewRole { get; set; }

        [Required]
        public List<SelectListItem> RoleOptions
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem("Choose new role", $"{string.Empty}", true, true),
                    new SelectListItem("Admin", "Admin"),
                    new SelectListItem("Cashier", "Cashier"),
                };
            }
        }
    }
}
