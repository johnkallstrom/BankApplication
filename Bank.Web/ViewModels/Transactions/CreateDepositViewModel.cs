using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels
{
    public class CreateDepositViewModel
    {
        [Required(ErrorMessage = "Please enter to account number.")]
        [Display(Name = "To")]
        public int AccountId { get; set; }

        [Required]
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Please enter amount.")]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency, ErrorMessage = "Please enter a valid amount.")]
        [Range(1, double.MaxValue, ErrorMessage = "Minimum amount allowed is 1.")]
        public decimal Amount { get; set; }

        public string DisplayBalance => Balance.ToString("C2");
    }
}
