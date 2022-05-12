using System.Collections.Generic;
using System.Linq;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MinTur.Models.In;
using MinTur.Models.Out;
using MinTur.WebApi.Filters;

namespace MinTur.WebApi.Controllers
{
    [EnableCors("AllowEverything")]
    [Route("api/administrators")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorManager _administratorManager;

        public AdministratorController(IAdministratorManager administratorManager)
        {
            _administratorManager = administratorManager;
        }

        [HttpGet]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult GetAll()
        {
            List<Administrator> retrievedAdministrators = _administratorManager.GetAllAdministrators();
            List<AdministratorBasicInfoModel> administratorBasicInfoModels = retrievedAdministrators.Select(administrator => new AdministratorBasicInfoModel(administrator)).ToList();
            return Ok(administratorBasicInfoModels);
        }

        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult GetSpecificAdministrator(int id) 
        {
            Administrator retrievedAdministrator = _administratorManager.GetAdministratorById(id);
            AdministratorBasicInfoModel administratorBasicInfoModel = new AdministratorBasicInfoModel(retrievedAdministrator);
            return Ok(administratorBasicInfoModel);
        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult DeleteAdministrator(int id)
        {
            _administratorManager.DeleteAdministratorById(id);
            return Ok(new { ResultMessage = $"Administrator {id} succesfuly deleted" });
        }

        [HttpPost]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult CreateAdministrator([FromBody] AdministratorIntentModel administratorIntentModel)
        {
            Administrator createdAdministrator = _administratorManager.RegisterAdministrator(administratorIntentModel.ToEntity());
            AdministratorBasicInfoModel confirmation = new AdministratorBasicInfoModel(createdAdministrator);
            return Created("api/resorts/" + createdAdministrator.Id, confirmation);
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(AdministratorAuthorizationFilter))]
        public IActionResult UpdateAdministrator(int id, [FromBody] AdministratorIntentModel administratorIntentModel)
        {
            Administrator updatedAdministrator = administratorIntentModel.ToEntity();
            updatedAdministrator.Id = id;

            Administrator retrievedAdministrator = _administratorManager.UpdateAdministrator(updatedAdministrator);
            AdministratorBasicInfoModel administratorBasicInfoModel = new AdministratorBasicInfoModel(retrievedAdministrator);
            return Ok(administratorBasicInfoModel);
        }

    }
}