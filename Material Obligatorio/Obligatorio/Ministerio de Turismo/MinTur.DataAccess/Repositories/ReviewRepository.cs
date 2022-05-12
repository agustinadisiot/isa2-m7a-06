using Microsoft.EntityFrameworkCore;
using MinTur.DataAccessInterface.Repositories;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using System;
using System.Linq;

namespace MinTur.DataAccess.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        protected DbContext Context { get; set; }

        public ReviewRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public Review GetReviewById(int reviewId)
        {
            if (!ReviewExists(reviewId))
                throw new ResourceNotFoundException("Could not find specified review");

            return Context.Set<Review>().AsNoTracking().Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public int StoreReview(Review review)
        {
            if (ReservationAlredyHasReview(review))
                throw new InvalidOperationException("You can only submit one review per reservation");

            StoreReviewInDb(review);
            return review.Id;
        }

        private void StoreReviewInDb(Review review)
        {
            Context.Set<Review>().Add(review);
            Context.SaveChanges();
            Context.Entry(review).State = EntityState.Detached;
        }

        private bool ReviewExists(int reviewId)
        {
            Review review = Context.Set<Review>().AsNoTracking().Where(r => r.Id == reviewId).FirstOrDefault();
            return review != null;
        }

        private bool ReservationAlredyHasReview(Review review)
        {
            Review retrievedReview = Context.Set<Review>().AsNoTracking().Where(r => r.ReservationId == review.ReservationId).FirstOrDefault();

            return retrievedReview != null;
        }

    }
}
