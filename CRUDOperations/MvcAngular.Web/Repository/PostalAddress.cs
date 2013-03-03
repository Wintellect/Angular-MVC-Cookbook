using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAngular.Web.Repository
{
    public class PostalAddress
    {
        public const int AddressLineMaxLength = 50;
        public const int CityMaxLength = 50;
        public const int StateProviceMaxLength = 50;
        public const int CountryMaxLength = 50;
        public const int PostalCodeMaxLength = 10;

        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int PostalAddressId { get; set; }
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}