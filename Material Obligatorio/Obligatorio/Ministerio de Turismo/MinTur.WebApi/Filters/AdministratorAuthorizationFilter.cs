using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using MinTur.BusinessLogicInterface.Security;
using System;

namespace MinTur.WebApi.Filters
{
    public class AdministratorAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AdministratorAuthorizationFilter(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];

            if (token == StringValues.Empty)
                context.Result = new JsonResult("Please send your authorization token") { StatusCode = 401 };
            else if (!Guid.TryParse(token, out Guid parsedToken))
                context.Result = new JsonResult("Invalid token format") { StatusCode = 400 };
            else if (!_authenticationManager.IsTokenValid(parsedToken))
                context.Result = new JsonResult("Your token either expired or is not valid") { StatusCode = 401 };
        }
    }
}
