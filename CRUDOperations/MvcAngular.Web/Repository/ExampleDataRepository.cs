using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAngular.Web.Repository
{
    public class ExampleDataRepository
    {
        public IEnumerable<Person> GetSomePeople()
        {
            using (var ctx = new ExampleDbContext())
            {
                return ctx.People.AsNoTracking().Take(30).ToList();
            }
        }

        public Person GetPerson(int personId)
        {
            using (var ctx = new ExampleDbContext())
            {
                return 
                    ctx
                        .People
                        .Include(p => p.EmailAddresses)
                        .Include(p => p.PostalAddresses)
                        .Include(p => p.PhoneNumbers)
                        .AsNoTracking()
                        .SingleOrDefault(p => p.PersonId == personId);
            }
        }
    }
}