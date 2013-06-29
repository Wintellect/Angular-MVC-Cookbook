using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MvcAngular.Web.Models;
using MvcAngular.Web.Models.Binders;
using MvcAngular.Web.Repository;

namespace MvcAngular.Web.API
{
    public class PeopleController : ApiController
    {
        public PersonResponse Get([ModelBinder] PeopleRequest model)
        {
            model = model ?? new PeopleRequest();
            var repository = new ExampleDataRepository();
            return repository.GetPeople(model);
        }

        public Person Get(int id)
        {
            var repository = new ExampleDataRepository();
            var person = repository.ReadPerson(id);
            if (person == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return person;
        }

        public void Post(Person person)
        {
            var repository = new ExampleDataRepository();
            repository.CreatePerson(person);
        }

        public void Put(Person person)
        {
            var repository = new ExampleDataRepository();
            repository.UpdatePerson(person);
        }

        public void Delete(int id)
        {
            var repository = new ExampleDataRepository();
            repository.DeletePerson(id);
        }
    }
}
