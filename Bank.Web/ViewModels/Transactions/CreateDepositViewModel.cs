using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels
{
    public class CreateDepositViewModel
    {
        [Required]
        [Display(Name = "To")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Please enter amount.")]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
