using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace TrueHomeActivities.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var jsonResult = new JsonResult(context.Exception.Message)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            if (exception is PropertyDisabledException ||
                exception is OtherActivityAtTheSameTimeException ||
                exception is CancelledActivityReScheduleException)
            {
                jsonResult.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            context.Result = jsonResult;
        }
    }
}
