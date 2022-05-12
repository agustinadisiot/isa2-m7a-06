using MinTur.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace MinTur.WebApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            try
            {
                throw context.Exception;
            }
            catch (ResourceNotFoundException e)
            {
                context.Result = new JsonResult(e.Message) { StatusCode = 404 };
            }
            catch (InvalidRequestDataException e)
            {
                context.Result = new JsonResult(e.Message) { StatusCode = 400 };
            }
            catch (InvalidOperationException e)
            {
                context.Result = new JsonResult(e.Message) { StatusCode = 409 };
            }
            catch (Exception)
            {
                context.Result = new JsonResult("We encountered some issues, try again later") { StatusCode = 500 };
            }
        }
    }
}
