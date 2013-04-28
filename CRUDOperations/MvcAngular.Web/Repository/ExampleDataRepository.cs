using System;
using System.Data;
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

        public Person ReadPerson(int personId)
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

        public void CreatePerson(Person person)
        {
            using (var ctx = new ExampleDbContext())
            {
                ctx.People.Add(person);
                ctx.SaveChanges();
            }
        }

        public void UpdatePerson(Person person)
        {
            using (var ctx = new ExampleDbContext())
            {
                ctx.People.Attach(person);
                ctx.Entry(person).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeletePerson(int personId)
        {
            using (var ctx = new ExampleDbContext())
            {
                var person = ctx.People.SingleOrDefault(p => p.PersonId == personId);
                if (person == null)
                {
                    throw new ObjectNotFoundException("Invalid person id.");
                }

                ctx.People.Remove(person);
                ctx.SaveChanges();
            }
        }
    }
}