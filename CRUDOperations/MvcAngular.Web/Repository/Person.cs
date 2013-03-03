using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace MvcAngular.Web.Repository
{
    public class Person
    {
        public const int TitleMaxLength = 50;
        public const int FirstNameMaxLength = 50;
        public const int MiddleNameMaxLength = 50;
        public const int LastNameMaxLength = 50;
        public const int SuffixMaxLength = 50;

        private ICollection<PostalAddress> _postalAddresses;
        private ICollection<PhoneNumber> _phoneNumbers;
        private ICollection<EmailAddress> _emailAddresses;

        public int PersonId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }

        public ICollection<PostalAddress> PostalAddresses
        {
            get { return _postalAddresses ?? (_postalAddresses = new Collection<PostalAddress>()); }
            set { _postalAddresses = value; }
        }
        public ICollection<PhoneNumber> PhoneNumbers
        {
            get { return _phoneNumbers ?? (_phoneNumbers = new Collection<PhoneNumber>()); }
            set { _phoneNumbers = value; }
        }
        public ICollection<EmailAddress> EmailAddresses
        {
            get { return _emailAddresses ?? (_emailAddresses = new Collection<EmailAddress>()); }
            set { _emailAddresses = value; }
        }
    }
}