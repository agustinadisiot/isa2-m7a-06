using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using MinTur.Models.In;
using MinTur.Models.Out;
using MinTur.WebApi.Controllers;
using Moq;
using System;

namespace MinTur.WebApi.Test.Controllers
{
    [TestClass]
    public class ReviewControllerTest
    {
        private Mock<IReviewManager> _reviewManagerMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _reviewManagerMock = new Mock<IReviewManager>(MockBehavior.Strict);
        }
        #endregion

        [TestMethod]
        public void CreateReviewCreatedAtTest()
        {
            ReviewIntentModel reviewIntentModel = CreateReviewIntentModel();
            Review createdReview = CreateReview(reviewIntentModel);

            _reviewManagerMock.Setup(r => r.RegisterReview(It.IsAny<Guid>(), It.IsAny<Review>())).Returns(createdReview);
            ReviewController reviewController = new ReviewController(_reviewManagerMock.Object);

            IActionResult result = reviewController.CreateReview(reviewIntentModel);
            CreatedResult createdResult = result as CreatedResult;

            _reviewManagerMock.VerifyAll();
            Assert.IsTrue(createdResult.StatusCode == StatusCodes.Status201Created);
            Assert.IsTrue(createdResult.Value.Equals(new ReviewDetailsModel(createdReview)));
        }

        #region Helpers
        public ReviewIntentModel CreateReviewIntentModel() 
        {
            return new ReviewIntentModel()
            {
                ReservationId = Guid.NewGuid(),
                Stars = 4,
                Text = "Muy bueno todo, volveria denuevo"
            };
        }
        public Review CreateReview(ReviewIntentModel reviewIntentModel) 
        {
            return new Review()
            {
                Text = reviewIntentModel.Text,
                Stars = reviewIntentModel.Stars,
                Name = "Pepe",
                Surname = "Perez"
            };
        }
        #endregion

    }
}
