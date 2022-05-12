using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using MinTur.Models.Out;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.WebApi.Controllers
{
    [EnableCors("AllowEverything")]
    [Route("api/regions")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionManager _regionManager;

        public RegionController(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Region> retrievedRegions = _regionManager.GetAllRegions();
            List<RegionBasicInfoModel> regionModels = retrievedRegions.Select(region => new RegionBasicInfoModel(region)).ToList();
            return Ok(regionModels);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetSpecificRegion(int id)
        {
            Region retrievedRegion = _regionManager.GetRegionById(id);
            RegionBasicInfoModel regionModel = new RegionBasicInfoModel(retrievedRegion);
            return Ok(regionModel);
        }

    }
}
