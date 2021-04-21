using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Mime;

namespace IdentityCreation
{
    public class CustomExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            context.Result = new ContentResult()
            {
                Content = exception.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ContentType = MediaTypeNames.Text.Plain
            };
            base.OnException(context);
        }
    }
}
