using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels.User
{
    public class UpdateUserRolesViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [DisplayName("Current role")]
        public string CurrentRole { get; set; }

        [Required]
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
