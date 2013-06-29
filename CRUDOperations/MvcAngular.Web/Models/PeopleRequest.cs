using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAngular.Web.Models
{
    public class PeopleRequest
    {
        public PeopleRequest()
        {
            PageSize = 20;
            PageIndex = 1;
            OrderBy = null;
            Descending = false;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string OrderBy { get; set; }
        public bool Descending { get; set; }

        public void Validate()
        {
            if (PageSize > 100)
            {
                throw new InvalidOperationException("Page size must be no greater than 100 records.");
            }
            if (PageSize < 1)
            {
                throw new InvalidOperationException("Page size must be greater than zero.");
            }

            if (PageIndex < 1)
            {
                throw new InvalidOperationException("Page index must be greater than zero.");
            }
        }
    }
}