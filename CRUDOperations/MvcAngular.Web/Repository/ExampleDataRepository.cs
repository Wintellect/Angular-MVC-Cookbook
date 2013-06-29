using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcAngular.Web.Models;

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

        public PersonResponse GetPeople(PeopleRequest request)
        {
            request.Validate();

            using (var ctx = new ExampleDbContext())
            {
                IQueryable<Person> query;

                query = ctx.People.AsNoTracking();

                var orderBy = (request.OrderBy ?? "").ToLower();
                switch (orderBy)
                {
                    default:
                        query =
                            request.Descending
                                ? query.OrderByDescending(p => p.LastName).ThenByDescending(p => p.FirstName)
                                : query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
                        break;
                    case "firstname":
                        query =
                            request.Descending
                                ? query.OrderByDescending(p => p.FirstName).ThenByDescending(p => p.LastName)
                                : query.OrderBy(p => p.FirstName).ThenBy(p => p.LastName);
                        break;
                    case "middlename":
                        query =
                            request.Descending
                                ? query.OrderByDescending(p => p.MiddleName).ThenByDescending(p => p.FirstName).ThenByDescending(p => p.LastName)
                                : query.OrderBy(p => p.MiddleName).ThenBy(p => p.FirstName).ThenBy(p => p.LastName);
                        break;
                    case "suffix":
                        query =
                            request.Descending
                                ? query.OrderByDescending(p => p.Suffix).ThenByDescending(p => p.LastName).ThenByDescending(p => p.FirstName)
                                : query.OrderBy(p => p.Suffix).ThenBy(p => p.LastName).ThenBy(p => p.FirstName);
                        break;
                    case "title":
                        query =
                            request.Descending
                                ? query.OrderByDescending(p => p.Title).ThenByDescending(p => p.LastName).ThenByDescending(p => p.FirstName)
                                : query.OrderBy(p => p.Title).ThenBy(p => p.LastName).ThenBy(p => p.FirstName);
                        break;
                }

                var results = 
                    query
                        .Skip((request.PageIndex - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .GroupBy(r => new { Total = query.Count() })
                        .ToList();

                if (results.Count == 0)
                {
                    return
                        new PersonResponse
                        {
                            Total = 0,
                            Page = 0,
                            Records = 0,
                            Rows = Enumerable.Empty<Person>().ToList()
                        };
                }

                int totalRecordCount = results[0].Key.Total;
                return new PersonResponse
                    {
                        Total = totalRecordCount / request.PageSize,
                        Page = request.PageIndex,
                        Records = totalRecordCount,
                        Rows = results[0].ToList()
                    };
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