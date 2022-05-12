using MinTur.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using MinTur.WebApi.Filters;

namespace MinTur.WebApi.Test.Filters
{
    [TestClass]
    public class ExceptionFilterTest
    {
        private ExceptionFilter _exceptionFilter;
        private Mock<ExceptionContext> _exceptionContestMock;

        [TestInitialize]
        public void SetUp() 
        {
            _exceptionFilter = new ExceptionFilter();
            ActionContext actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };
            _exceptionContestMock = new Mock<ExceptionContext>(actionContext, new List<IFilterMetadata>());
        }
        
        [TestMethod]
        public void ResourceNotFoundReturns404() 
        {
            string exceptionMessage = "We couldnt find what you are looking for";
            _exceptionContestMock.SetupAllProperties();
            _exceptionContestMock.Setup(c => c.Exception).Returns(new ResourceNotFoundException(exceptionMessage));

            _exceptionFilter.OnException(_exceptionContestMock.Object);
            JsonResult result = _exceptionContestMock.Object.Result as JsonResult;

            Assert.IsTrue(result.StatusCode == StatusCodes.Status404NotFound);
            Assert.IsTrue(result.Value.ToString().Equals(exceptionMessage));
        }

        [TestMethod]
        public void InvalidRequestDataReturns400()
        {
            string exceptionMessage = "Invalid request";
            _exceptionContestMock.SetupAllProperties();
            _exceptionContestMock.Setup(c => c.Exception).Returns(new InvalidRequestDataException(exceptionMessage));

            _exceptionFilter.OnException(_exceptionContestMock.Object);
            JsonResult result = _exceptionContestMock.Object.Result as JsonResult;

            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
            Assert.IsTrue(result.Value.ToString().Equals(exceptionMessage));
        }

        [TestMethod]
        public void InvalidOperationReturns409()
        {
            string exceptionMessage = "Invalid operation";
            _exceptionContestMock.SetupAllProperties();
            _exceptionContestMock.Setup(c => c.Exception).Returns(new InvalidOperationException(exceptionMessage));

            _exceptionFilter.OnException(_exceptionContestMock.Object);
            JsonResult result = _exceptionContestMock.Object.Result as JsonResult;

            Assert.IsTrue(result.StatusCode == StatusCodes.Status409Conflict);
            Assert.IsTrue(result.Value.ToString().Equals(exceptionMessage));
        }

        [TestMethod]
        public void GenericExceptionReturns500()
        {
            _exceptionContestMock.SetupAllProperties();
            _exceptionContestMock.Setup(c => c.Exception).Returns(new Exception());

            _exceptionFilter.OnException(_exceptionContestMock.Object);
            JsonResult result = _exceptionContestMock.Object.Result as JsonResult;

            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
            Assert.IsTrue(result.Value.ToString().Equals("We encountered some issues, try again later"));
        }
    }
}
