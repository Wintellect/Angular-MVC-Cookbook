using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Elmah;
using IExceptionFilter = System.Web.Http.Filters.IExceptionFilter;

namespace MvcAngular.Web
{
    public class FilterConfig
    {
        private static ErrorFilterConfiguration _config;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MvcErrorAttribute());
        }

        public static void RegisterGlobalFilters(HttpFilterCollection filters)
        {
            filters.Add(new WebApiErrorHandler());
        }

        private class WebApiErrorHandler : System.Web.Http.Filters.ExceptionFilterAttribute
        {
            public override void OnException(HttpActionExecutedContext actionExecutedContext)
            {
                base.OnException(actionExecutedContext);

                var ex = actionExecutedContext.Exception;
                var httpContext = HttpContext.Current;
                if (httpContext != null && (RaiseErrorSignal(ex, httpContext) || IsFiltered(ex, httpContext)))
                {
                    return;
                }

                LogException(ex, httpContext);
            }
        }

        private class MvcErrorAttribute : System.Web.Mvc.HandleErrorAttribute
        {
            public override void OnException(ExceptionContext context)
            {
                base.OnException(context);

                if (!context.ExceptionHandled)
                {
                    return;
                }

                var ex = context.Exception;
                var httpContext = context.HttpContext.ApplicationInstance.Context;
                if (httpContext != null && (RaiseErrorSignal(ex, httpContext) || IsFiltered(ex, httpContext)))
                {
                    return;
                }

                LogException(ex, httpContext);
            }
        }

        private static bool RaiseErrorSignal(Exception e, HttpContext context)
        {
            var signal = ErrorSignal.FromContext(context);
            if (signal == null)
            {
                return false;
            }

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

        private static void LogException(Exception ex, HttpContext context)
        {
            ErrorLog.GetDefault(context).Log(new Error(ex, context));
        }
    }
}