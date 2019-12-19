using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using API.Models;
using Newtonsoft.Json;

namespace API
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var message = new ErrorMessageModel();
            var now = DateTime.Now;
            message.LogId = now.Hour * 10000 + now.Minute * 100 + now.Second;
            message.Message = context.Exception.InnerException?.Message ?? context.Exception.Message;

            var response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(message));
            context.Result = new ResponseMessageResult(response);
        }
    }
}