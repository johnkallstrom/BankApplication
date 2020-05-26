using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels
{
    public class CreateTransferViewModel
    {
        [Required(ErrorMessage = "Please enter from account number.")]
        [Display(Name = "From")]
        public int FromAccountId { get; set; }

        [Required]
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Please enter to account number.")]
        [Display(Name = "To")]
        [Range(1, int.MaxValue, ErrorMessage = ("Please enter a valid account number."))]
        public int ToAccountId { get; set; }

        [Required(ErrorMessage = "Please enter amount.")]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency, ErrorMessage = "Please enter a valid amount.")]
        public decimal Amount { get; set; }

        public string DisplayBalance => Balance.ToString("C2");
    }
}
