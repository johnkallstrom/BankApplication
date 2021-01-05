using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels
{
    public class AccountTransferViewModel
    {
        [Required(ErrorMessage = "Please enter from account number.")]
        [Display(Name = "From")]
        public int FromAccountId { get; set; }

        [Required]
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }

        public List<SelectListItem> ToAccountOptions { get; set; }

        [Required(ErrorMessage = "Please enter to account number.")]
        [Display(Name = "To")]
        public string ToAccountId { get; set; }

        [Required(ErrorMessage = "Please enter amount.")]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency, ErrorMessage = "Please enter a valid amount.")]
        [Range(1, double.MaxValue, ErrorMessage = "Minimum amount allowed is 1.")]
        public decimal Amount { get; set; }

        public string DisplayBalance => Balance.ToString("C2");
    }
}
