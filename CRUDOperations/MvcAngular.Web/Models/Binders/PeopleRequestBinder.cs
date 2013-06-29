
using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace MvcAngular.Web.Models.Binders
{
    public class PeopleRequestBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var req = new PeopleRequest();

            req.PageSize = GetValue(req.PageSize, bindingContext, "pageSize", "rows");
            req.PageIndex = GetValue(req.PageIndex, bindingContext, "pageIndex", "page");
            req.OrderBy = GetValue(req.OrderBy, bindingContext, "orderBy", "sidx");
            req.Descending = GetValue("", bindingContext, "descending", "sord") == "desc";

            bindingContext.Model = req;
            return true;
        }

        private int GetValue(int defaultValue, ModelBindingContext bindingContext, params string[] keyNames)
        {
            foreach (var keyName in keyNames)
            {
                var valueProv = bindingContext.ValueProvider.GetValue(keyName);
                if (valueProv != null)
                {
                    return Convert.ToInt32(valueProv.RawValue);
                }
            }
            return defaultValue;
        }

        private string GetValue(string defaultValue, ModelBindingContext bindingContext, params string[] keyNames)
        {
            foreach (var keyName in keyNames)
            {
                var valueProv = bindingContext.ValueProvider.GetValue(keyName);
                if (valueProv != null)
                {
                    return Convert.ToString(valueProv.RawValue);
                }
            }
            return defaultValue;
        }
    }
}