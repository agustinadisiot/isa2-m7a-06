using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.BusinessLogic.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using Moq;
using System;

namespace MinTur.BusinessLogic.Test.ResourceManagers
{
    [TestClass]
    public class ReviewManagerTest
    {
        private Mock<IRepositoryFacade> _repositoryFacadeMock;
        private Mock<Review> _reviewMock;
        private Mock<Resort> _resortMock;

        #region SetUp
        [TestInitialize]
        public void SetUp() 
        {
            _repositoryFacadeMock = new Mock<IRepositoryFacade>(MockBehavior.Strict);
            _reviewMock = new Mock<Review>(MockBehavior.Strict);
            _resortMock = new Mock<Resort>(MockBehavior.Strict);
        }
        #endregion
        
        [TestMethod]
        public void RegisterReviewReturnsAsExpected() 
        {
            int reviewId = 6;
            Guid reservationId = Guid.NewGuid();
            Reservation retrievedReservation = CreateReservation();
            Resort retrievedResort = CreateResortWithSpecificId(retrievedReservation.Resort.Id);
            Review createdReview = CreateReview(reviewId, retrievedReservation);

            _reviewMock.SetupAllProperties();

            _repositoryFacadeMock.Setup(r => r.GetReservationById(reservationId)).Returns(retrievedReservation);
            _repositoryFacadeMock.Setup(r => r.GetResortById(retrievedReservation.Resort.Id)).Returns(_resortMock.Object);
            _reviewMock.Setup(r => r.ValidOrFail());
            _resortMock.Setup(r => r.UpdateResortPunctuation(_reviewMock.Object));
            _repositoryFacadeMock.Setup(r => r.StoreReview(_reviewMock.Object)).Returns(reviewId);
            _repositoryFacadeMock.Setup(r => r.UpdateResort(_resortMock.Object));
            _repositoryFacadeMock.Setup(r => r.GetReviewById(reviewId)).Returns(createdReview);

            ReviewManager reviewManager = new ReviewManager(_repositoryFacadeMock.Object);
            Review retrievedReview = reviewManager.RegisterReview(reservationId, _reviewMock.Object);

            _repositoryFacadeMock.VerifyAll();
            _reviewMock.VerifyAll();
            _resortMock.VerifyAll();
            Assert.AreEqual(createdReview, retrievedReview);
        }

        #region Helpers
        private Review CreateReview(int reviewId, Reservation reservation) 
        {
            return new Review()
            {
                Id = reviewId,
                Name = reservation.Name,
                Surname = reservation.Surname,
                Stars = 5,
                Text = "El mejor del mundo"
            };
        }
        private Reservation CreateReservation() 
        {
            return new Reservation()
            {
                Name = "Pepe",
                Surname = "Perez",
                Resort = new Resort()
                {
                    Id = 3
                }
            };
        }
        private Resort CreateResortWithSpecificId(int id)
        {
            return new Resort()
            {
                Id = id,
                Stars = 4
            };
        }
        #endregion

    }
}
