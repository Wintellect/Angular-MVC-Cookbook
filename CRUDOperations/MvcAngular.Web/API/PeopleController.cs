using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcAngular.Web.Repository;

namespace MvcAngular.Web.API
{
    public class PeopleController : ApiController
    {
        public IEnumerable<Person> Get()
        {
            var repository = new ExampleDataRepository();
            return repository.GetSomePeople();
        }

        public Person Get(int id)
        {
            var repository = new ExampleDataRepository();
            var person = repository.GetPerson(id);
            if (person == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return person;
        }
    }
}
