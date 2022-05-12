using MinTur.BusinessLogicInterface.ResourceManagers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;
using MinTur.Models.Out;
using MinTur.Models.In;
using System.Linq;
using MinTur.WebApi.Filters;

namespace MinTur.WebApi.Controllers
{
    [EnableCors("AllowEverything")]
    [Route("api/touristPoints")]
    [ApiController]
    public class TouristPointController : ControllerBase
    {
        private readonly ITouristPointManager _touristPointManager;

        public TouristPointController(ITouristPointManager touristPointManager)
        {
            _touristPointManager = touristPointManager;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] TouristPointSearchModel touristPointsSearchModel)
        {
            List<TouristPoint> retrievedTouristPoints = _touristPointManager.GetAllTouristPointsByMatchingCriteria(touristPointsSearchModel.ToEntity());
            List<TouristPointBasicInfoModel> touristPointsModels = retrievedTouristPoints.Select(
                touristPoint => new TouristPointBasicInfoModel(touristPoint)).ToList();

            return Ok(touristPointsModels);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetSpecificTouristPoint(int id)
        {
            TouristPoint retrievedTouristPoint = _touristPointManager.GetTouristPointById(id);
            TouristPointBasicInfoModel touristPointModel = new TouristPointBasicInfoModel(retrievedTouristPoint);
            return Ok(touristPointModel);
        }

        [HttpPost]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult CreateTouristPoint([FromBody] TouristPointIntentModel touristPointIntentModel)
        {
            TouristPoint registeredTouristPoint = _touristPointManager.RegisterTouristPoint(touristPointIntentModel.ToEntity());
            TouristPointBasicInfoModel touristPointModel = new TouristPointBasicInfoModel(registeredTouristPoint);
            return Created("api/touristPoints/" + touristPointModel.Id, touristPointModel);
        }

    }
}