using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Elmah;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            filters.Add(new WebApiErrorFilter());
        }

        private class WebApiErrorFilter : IExceptionFilter
        {
            public bool AllowMultiple
            {
                get { return true; }
            }

            public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
            {
                var statusCode = HttpStatusCode.InternalServerError;
                var responseObject = new JObject();
                dynamic responseData = responseObject;
                var ex = actionExecutedContext.Exception;

                responseData.message = ex.Message;
                AddDiagnosticInformation(ex, responseObject);

                string jsonText = JsonConvert.SerializeObject(responseData, WebApiConfig.JsonSerializerSettings);
                var httpContent = new StringContent(jsonText, Encoding.UTF8);
                httpContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
                    {
                        CharSet = "utf-8"
                    };

                var httpResponse =
                    new HttpResponseMessage
                    {
                        StatusCode = statusCode,
                        Content = httpContent,
                    };

                actionExecutedContext.Response = httpResponse;

                var httpContext = HttpContext.Current;
                if (!(httpContext != null && (RaiseErrorSignal(ex, httpContext) || IsFiltered(ex, httpContext))))
                {
                    LogException(ex, httpContext);
                }

                return Task.FromResult(false);
            }

            [Conditional("DEBUG")]
            private void AddDiagnosticInformation(Exception ex, JObject responseObject)
            {
                dynamic responseData = responseObject;
                var exLst = new List<Exception>();
                for (var x = ex; x != null; x = x.InnerException)
                {
                    exLst.Add(x);
                }
                responseData.Exceptions = new JArray(
                    exLst
                        .Select(
                            x =>
                            JObject.FromObject(
                                new
                                    {
                                        errorType = x.GetType().FullName,
                                        message = x.Message,
                                    }))
                        .ToList());
                responseData.StackTrace = ex.StackTrace;
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
                if (!(httpContext != null && (RaiseErrorSignal(ex, httpContext) || IsFiltered(ex, httpContext))))
                {
                    LogException(ex, httpContext);
                }
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