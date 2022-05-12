using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MinTur.BusinessLogicInterface.Security;
using System;
using System.Collections.Generic;
using MinTur.WebApi.Filters;

namespace MinTur.WebApi.Test.Filters
{
    [TestClass]
    public class AdministratorAuthorizationFilterTest
    {
        private AdministratorAuthorizationFilter _filter;
        private Mock<AuthorizationFilterContext> _authorizationFilterContextMock;
        private Mock<IAuthenticationManager> _authenticationManagerMock;

        [TestInitialize]
        public void SetUp() 
        {
            _authenticationManagerMock = new Mock<IAuthenticationManager>();
            _filter = new AdministratorAuthorizationFilter(_authenticationManagerMock.Object);
            ActionContext actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };
            _authorizationFilterContextMock = new Mock<AuthorizationFilterContext>(actionContext, new List<IFilterMetadata>());
        }

        [TestMethod]
        public void NoTokenIsSentReturns401() 
        {
            _authorizationFilterContextMock.SetupAllProperties();

            _filter.OnAuthorization(_authorizationFilterContextMock.Object);
            JsonResult result = _authorizationFilterContextMock.Object.Result as JsonResult;
            Assert.IsTrue(result.StatusCode == StatusCodes.Status401Unauthorized);
        }

        [TestMethod]
        public void InvalidTokenSentReturns401()
        {
            _authorizationFilterContextMock.SetupAllProperties();
            _authorizationFilterContextMock.Object.HttpContext.Request.Headers.Add("Authorization", Guid.NewGuid().ToString());

            _authenticationManagerMock.Setup(a => a.IsTokenValid(It.IsAny<Guid>())).Returns(false);
            _filter.OnAuthorization(_authorizationFilterContextMock.Object);

            JsonResult result = _authorizationFilterContextMock.Object.Result as JsonResult;
            Assert.IsTrue(result.StatusCode == StatusCodes.Status401Unauthorized);
        }

        [TestMethod]
        public void InvalidTokenFormatSentReturns400()
        {
            _authorizationFilterContextMock.SetupAllProperties();
            _authorizationFilterContextMock.Object.HttpContext.Request.Headers.Add("Authorization", "not a guid");

            _filter.OnAuthorization(_authorizationFilterContextMock.Object);
            JsonResult result = _authorizationFilterContextMock.Object.Result as JsonResult;
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void ValidTokenPassesFilter()
        {
            _authorizationFilterContextMock.SetupAllProperties();
            _authorizationFilterContextMock.Object.HttpContext.Request.Headers.Add("Authorization", Guid.NewGuid().ToString());

            _authenticationManagerMock.Setup(a => a.IsTokenValid(It.IsAny<Guid>())).Returns(true);
            _filter.OnAuthorization(_authorizationFilterContextMock.Object);

            Assert.IsNull(_authorizationFilterContextMock.Object.Result);
        }
    }
}
