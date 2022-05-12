using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MinTur.Models.In;
using MinTur.Models.Out;
using System.Collections.Generic;
using System.Linq;
using MinTur.WebApi.Filters;
using MinTur.BusinessLogicInterface.Pricing;

namespace MinTur.WebApi.Controllers
{
    [EnableCors("AllowEverything")]
    [Route("api/resorts")]
    [ApiController]
    public class ResortController : ControllerBase
    {
        private readonly IResortManager _resortManager;
        private readonly IResortPricingCalculator _resortPricingCalculator;

        public ResortController(IResortManager resortManager, IResortPricingCalculator resortPricingCalculator)
        {
            _resortManager = resortManager;
            _resortPricingCalculator = resortPricingCalculator;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] ResortSearchModel resortSearchModel, [FromQuery] AccommodationIntentModel accommodationModel)
        {
            if (resortSearchModel.ClientSearch)
            {
                List<Resort> retrievedResorts = _resortManager.GetAllResortsForAccommodationByMatchingCriteria(accommodationModel.ToEntity(), resortSearchModel.ToEntity());
                List<ResortSearchResultModel> resortModels = retrievedResorts.Select(
                    resort => new ResortSearchResultModel(resort)
                    {
                        TotalPrice = _resortPricingCalculator.CalculateTotalPriceForAccommodation(resort, accommodationModel.ToEntity())
                    }).ToList();

                return Ok(resortModels);
            }
            else
            {
                List<Resort> retrievedResorts = _resortManager.GetAllResortsByMatchingCriteria(resortSearchModel.ToEntity());
                List<ResortDetailsModel> resortsModels = retrievedResorts.Select(resort => new ResortDetailsModel(resort)).ToList();
                return Ok(resortsModels);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetSpecificResort(int id)
        {
            Resort retrievedResort = _resortManager.GetResortById(id);
            ResortDetailsModel resortDetailsModel = new ResortDetailsModel(retrievedResort);
            return Ok(resortDetailsModel);
        }

        [HttpPatch("{id:int}")]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult UpdateResortAvailability(int id, [FromBody] ResortPartialUpdateModel resortUpdateModel)
        {
            Resort updatedResort = _resortManager.UpdateResortAvailability(id, resortUpdateModel.GetNewAvailabilty());
            ResortDetailsModel resortDetailsModel = new ResortDetailsModel(updatedResort);
            return Ok(resortDetailsModel);
        }

        [HttpPost]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult CreateResort([FromBody] ResortIntentModel resortIntentModel)
        {
            Resort createdResort = _resortManager.RegisterResort(resortIntentModel.ToEntity());
            ResortDetailsModel confirmation = new ResortDetailsModel(createdResort);
            return Created("api/resorts/" + createdResort.Id, confirmation);
        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult DeleteResort(int id)
        {
            _resortManager.DeleteResortById(id);
            return Ok(new { ResultMessage = $"Resort {id} succesfuly deleted" });
        }

    }
}