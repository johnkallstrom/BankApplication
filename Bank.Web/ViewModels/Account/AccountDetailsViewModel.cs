using System;
using System.Collections.Generic;

namespace Bank.Web.ViewModels
{
    public class AccountDetailsViewModel
    {
        public int AccountId { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();

        public AccountDetailsViewModel(
            int accountId, 
            DateTime created, 
            decimal balance, 
            int currentPage, 
            int pageSize, 
            int totalPages, 
            List<TransactionViewModel> transactions)
        {
            AccountId = accountId;
            Created = created;
            Balance = balance;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            Transactions = transactions;
        }

        public string DisplayCreated => Created.ToShortDateString();
        public string DisplayBalance => Balance.ToString("C2");
        public bool EnablePrevButton() => CurrentPage > 1 ? true : false;
        public bool EnableNextButton() => CurrentPage < TotalPages ? true : false;
    }
}
