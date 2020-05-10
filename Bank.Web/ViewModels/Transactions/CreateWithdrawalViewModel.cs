﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels
{
    public class CreateWithdrawalViewModel
    {
        [Required]
        [Display(Name = "From")]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Please enter amount.")]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency, ErrorMessage = "Please enter a number.")]
        [Range(1, double.MaxValue, ErrorMessage = "The amount must be higher than 0.")]
        public decimal Amount { get; set; }
    }
}