using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAngular.Web.Repository
{
    public class EmailAddress
    {
        public const int AddressMaxLength = 254;

        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int EmailAddressId { get; set; }
        public string Address { get; set; }
    }
}