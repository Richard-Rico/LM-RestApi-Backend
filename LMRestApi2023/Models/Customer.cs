using System;
using System.Collections.Generic;

namespace LMRestApi2023.Models
{
    public partial class Customer
    {
        public Customer()
        {

        }

        public string CustomerId { get; set; } = null!;
        public string CompanyName { get; set; } = null!;

        public string VauvaIka { get; set; } = null!;
        public string SynnytysViikko { get; set; } = null!;
        public string SynnytysResume { get; set; } = null!;
        public string SynnytysHaaste { get; set; } = null!;

        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }

    }
}