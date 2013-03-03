using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAngular.Web.Repository
{
    public class PhoneNumber
    {
        public const int NumberMaxLength = 40;
        public const int NumberTypeMaxLength = 10;

        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int PhoneNumberId { get; set; }
        public string Number { get; set; }
        public string NumberType { get; set; }
    }
}