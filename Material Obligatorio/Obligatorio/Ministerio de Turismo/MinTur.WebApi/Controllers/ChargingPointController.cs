using MinTur.BusinessLogicInterface.ResourceManagers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MinTur.Domain.BusinessEntities;
using MinTur.Models.Out;
using MinTur.Models.In;
using MinTur.WebApi.Filters;

namespace MinTur.WebApi.Controllers
{
    [EnableCors("AllowEverything")]
    [Route("api/chargingPoints")]
    [ApiController]
    public class ChargingPointController : ControllerBase
    {
        private readonly IChargingPointManager _chargingPointManager;

        public ChargingPointController(IChargingPointManager chargingPointManager)
        {
            _chargingPointManager = chargingPointManager;
        }
        

        [HttpGet("{id:int}")]
        public IActionResult GetSpecificChargingPoint(int id)
        {
            ChargingPoint retrievedChargingPoint = _chargingPointManager.GetChargingPointById(id);
            ChargingPointBasicInfoModel chargingPointModel = new ChargingPointBasicInfoModel(retrievedChargingPoint);
            return Ok(chargingPointModel);
        }

        [HttpPost]
        //[ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult CreateChargingPoint([FromBody] ChargingPointIntentModel chargingPointIntentModel)
        {
            ChargingPoint registeredChargingPoint = _chargingPointManager.RegisterChargingPoint(chargingPointIntentModel.ToEntity());
            ChargingPointBasicInfoModel chargingPointModel = new ChargingPointBasicInfoModel(registeredChargingPoint);
            return Created("api/chargingPoint/" + chargingPointModel.Id, chargingPointModel);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteChargingPoint([FromRoute] int id)
        {
            ChargingPoint registeredChargingPoint = _chargingPointManager.DeleteChargingPoint(id);
            return Ok(registeredChargingPoint);
        }

    }
}