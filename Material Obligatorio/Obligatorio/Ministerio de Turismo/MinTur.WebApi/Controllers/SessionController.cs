using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MinTur.Models.In;
using MinTur.BusinessLogicInterface.Security;
using System;

namespace MinTur.WebApi.Controllers
{
    [EnableCors("AllowEverything")]
    [Route("api")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;

        public SessionController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] AdministratorIntentModel administratorIntentModel) 
        {
            Guid generatedToken = _authenticationManager.Login(administratorIntentModel.ToEntity());
            return Ok(generatedToken);
        }


    }
}
