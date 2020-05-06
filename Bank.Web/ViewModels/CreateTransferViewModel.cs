using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels
{
    public class CreateTransferViewModel
    {
        [Required]
        [Display(Name = "From")]
        public int FromAccountId { get; set; }
        [Required]
        [Display(Name = "To")]
        public int ToAccountId { get; set; }
        [Required(ErrorMessage = "Please enter amount")]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency, ErrorMessage = "Please enter a number")]
        [Range(1, double.MaxValue, ErrorMessage = "The amount must be higher than 0")]
        public decimal Amount { get; set; }
    }
}
