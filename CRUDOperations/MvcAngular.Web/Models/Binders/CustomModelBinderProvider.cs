using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using IModelBinder = System.Web.Http.ModelBinding.IModelBinder;

namespace MvcAngular.Web.Models.Binders
{
    public class CustomModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            if (modelType == typeof (PeopleRequest))
            {
                return new PeopleRequestBinder();
            }

            return null;
        }
    }
}