using Microsoft.Azure.Search;

namespace Bank.Infrastructure.SearchModels
{
    public class CustomerSearch
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string CustomerStringId { get; set; }

        [IsFilterable, IsSortable]
        public int CustomerId { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Gender { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Givenname { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Surname { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Streetaddress { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string City { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Zipcode { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Country { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string CountryCode { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string NationalId { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Telephonecountrycode { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Telephonenumber { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Emailaddress { get; set; }
    }
}
