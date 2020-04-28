using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your email.")]
        [Display(Name = "Username")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
