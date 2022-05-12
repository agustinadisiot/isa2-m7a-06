using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.DataAccess.Contexts;
using MinTur.DataAccess.Repositories;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using System;

namespace MinTur.DataAccess.Test.Repositories
{
    [TestClass]
    public class ReviewRepositoryTest
    {
        private ReviewRepository _repository;
        private NaturalUruguayContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.Memory);
            _repository = new ReviewRepository(_context);
        }

        [TestMethod]
        public void GetReviewByIdReturnsAsExpected()
        {
            Review review = new Review()
            {
                Name = "Pepe",
                Surname = "Perez",
                Text = "Maso maso",
                Stars = 2,
                ReservationId = Guid.NewGuid()
            };
            _context.Set<Review>().Add(review);
            _context.SaveChanges();
            _context.Entry(review).State = EntityState.Detached;

            Review retrievedReview = _repository.GetReviewById(review.Id);
            Assert.AreEqual(review, retrievedReview);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetReviewByIdWhichDoesntExist()
        {
            _repository.GetReviewById(-6);
        }

        [TestMethod]
        public void StoreReviewReturnsAsExpected()
        {
            Review review = new Review()
            {
                Name = "Pepe",
                Surname = "Perez",
                Text = "Maso maso",
                Stars = 2,
                ReservationId = Guid.NewGuid()
            };
            int newReviewId = _repository.StoreReview(review);

            Assert.AreEqual(review.Id, newReviewId);
            Assert.IsNotNull(_context.Reviews.Find(newReviewId));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StoreMoreThanOneReviewPerReservation() 
        {
            Guid sharedGuid = Guid.NewGuid();

            Review review1 = new Review()
            {
                Name = "Pepe",
                Surname = "Perez",
                Text = "Maso maso",
                Stars = 2,
                ReservationId = sharedGuid
            };
            _context.Add(review1);
            _context.SaveChanges();

            Review review2 = new Review()
            {
                Name = "Juan",
                Surname = "Juanes",
                Text = "Ni muy muy ni tan tan",
                Stars = 2,
                ReservationId = sharedGuid
            };

            _repository.StoreReview(review2);
        }

    }
}
