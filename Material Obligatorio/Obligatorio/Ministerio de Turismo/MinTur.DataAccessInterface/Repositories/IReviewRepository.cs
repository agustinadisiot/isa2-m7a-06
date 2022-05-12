using MinTur.Domain.BusinessEntities;

namespace MinTur.DataAccessInterface.Repositories
{
    public interface IReviewRepository
    {
        int StoreReview(Review review);
        Review GetReviewById(int reviewId);
    }
}
