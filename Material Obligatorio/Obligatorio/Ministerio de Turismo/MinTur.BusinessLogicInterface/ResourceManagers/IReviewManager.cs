using MinTur.Domain.BusinessEntities;
using System;

namespace MinTur.BusinessLogicInterface.ResourceManagers
{
    public interface IReviewManager
    {
        Review RegisterReview(Guid reservationId, Review review);
    }
}
