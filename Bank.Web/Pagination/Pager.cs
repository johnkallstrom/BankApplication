using System;

namespace Bank.Web.Pagination
{
    public class Pager
    {
        public int TotalItems { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public bool ShowPreviousButton { get; set; }

        public bool ShowNextButton { get; set; }

        public Pager(int totalItems, int currentPage = 1, int pageSize = 50)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

            if (currentPage < 1)
                currentPage = 1;

            if (currentPage > totalPages)
                currentPage = totalPages;

            bool showPreviousButton = currentPage > 1 ? true : false;
            bool showNextButton = currentPage < totalPages ? true : false;

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            ShowPreviousButton = showPreviousButton;
            ShowNextButton = showNextButton;
        }
    }
}
