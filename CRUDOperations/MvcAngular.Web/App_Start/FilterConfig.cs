using System;
using System.Web;
using System.Web.Mvc;
using Elmah;
using MvcAngular.Web.Repository;

namespace MvcAngular.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyHandleErrorAttribute());
        }

        private class MyHandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
        {
            private static ErrorFilterConfiguration _config;

            public override void OnException(ExceptionContext context)
            {
                base.OnException(context);

                if (!context.ExceptionHandled) // if unhandled, will be logged anyhow
                    return;

                var e = context.Exception;
                var httpContext = context.HttpContext.ApplicationInstance.Context;
                if (httpContext != null &&
                    (RaiseErrorSignal(e, httpContext) // prefer signaling, if possible
                     || IsFiltered(e, httpContext))) // filtered?
                    return;

                LogException(e, httpContext);
            }

            private static bool RaiseErrorSignal(Exception e, HttpContext context)
            {
                var signal = ErrorSignal.FromContext(context);
                if (signal == null)
                    return false;
                signal.Raise(e, context);
                return true;
            }

            private static bool IsFiltered(Exception e, HttpContext context)
            {
                if (_config == null)
                {
                    _config = context.GetSection("elmah/errorFilter") as ErrorFilterConfiguration
                              ?? new ErrorFilterConfiguration();
                }

                var testContext = new ErrorFilterModule.AssertionHelperContext(e, context);
                return _config.Assertion.Test(testContext);
            }

            private static void LogException(Exception e, HttpContext context)
            {
                ErrorLog.GetDefault(context).Log(new Error(e, context));
            }
        }
    }
}