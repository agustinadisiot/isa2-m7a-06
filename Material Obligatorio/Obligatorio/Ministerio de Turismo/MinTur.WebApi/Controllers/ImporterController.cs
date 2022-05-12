using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MinTur.BusinessLogicInterface.Importing;
using MinTur.Domain.Importing;
using MinTur.Models.In;
using MinTur.Models.Out;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.WebApi.Controllers
{
    [EnableCors("AllowEverything")]
    [Route("api/importers")]
    [ApiController]
    public class ImporterController : ControllerBase
    {
        private readonly IImporterManager _importerManager;

        public ImporterController(IImporterManager importManager)
        {
            _importerManager = importManager;
        }

        [HttpGet]
        public IActionResult GetImporters()
        {
            List<IImporterAdapter> importers = _importerManager.GetImporters();
            List<ImporterDetailModel> importerDetailsModel = importers.Select(i => new ImporterDetailModel(i)).ToList();
            return Ok(importerDetailsModel);
        }

        [HttpPost]
        [Route("importResources")]
        public IActionResult Import([FromBody] ImporterUsageModel importerUsageModel)
        {
            ImportingResult importingResult = _importerManager.ImportResources(importerUsageModel.ToEntity());
            ImportingResultModel importingResultModel = new ImportingResultModel(importingResult);
            return Ok(importingResultModel);
        }

    }
}
