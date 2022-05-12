using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using MinTur.Models.In;
using MinTur.Models.Out;

namespace MinTur.WebApi.Controllers
{
    [EnableCors("AllowEverything")]
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewManager _reviewManager;

        public ReviewController(IReviewManager reviewManager)
        {
            _reviewManager = reviewManager;
        }

        [HttpPost]
        public IActionResult CreateReview([FromBody] ReviewIntentModel reviewIntentModel) 
        {
            Review createdReview = _reviewManager.RegisterReview(reviewIntentModel.ReservationId, reviewIntentModel.ToEntity());
            ReviewDetailsModel reviewDetailsModel = new ReviewDetailsModel(createdReview);
            return Created("api/reviews/" + reviewDetailsModel.Id, reviewDetailsModel);
        }

    }
}
