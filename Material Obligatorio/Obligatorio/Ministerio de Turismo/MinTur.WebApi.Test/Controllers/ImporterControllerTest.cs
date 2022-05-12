using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.BusinessLogicInterface.Importing;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.Importing;
using MinTur.Models.In;
using MinTur.Models.Out;
using MinTur.WebApi.Controllers;
using Moq;
using System.Collections.Generic;

namespace MinTur.WebApi.Test.Controllers
{
    [TestClass]
    public class ImporterControllerTest
    {
        private Mock<IImporterManager> _importManagerMock;
        private List<ImporterDetailModel> _expectedImporters;
        private ImportingResultModel _expectedImportingResult;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _importManagerMock = new Mock<IImporterManager>(MockBehavior.Strict);
            _expectedImporters = new List<ImporterDetailModel>();
        }
        #endregion

        [TestMethod]
        public void GetImportersOkTest()
        {
            _importManagerMock.Setup(i => i.GetImporters()).Returns(GetImportersList());
            ImporterController importerController = new ImporterController(_importManagerMock.Object);

            IActionResult result = importerController.GetImporters();
            OkObjectResult okResult = result as OkObjectResult;
            List<ImporterDetailModel> responseModel = okResult.Value as List<ImporterDetailModel>;

            _importManagerMock.VerifyAll();
            CollectionAssert.AreEquivalent(_expectedImporters, responseModel);
        }

        [TestMethod]
        public void ImportOkTest()
        {
            ImporterUsageModel importerUsageModel = CreateImporterUsageModel();
            
            _importManagerMock.Setup(i => i.ImportResources(It.IsAny<ImportingInput>())).Returns(CreateImportingResult());
            ImporterController importerController = new ImporterController(_importManagerMock.Object);

            IActionResult result = importerController.Import(importerUsageModel);
            OkObjectResult okResult = result as OkObjectResult;
            ImportingResultModel resultModel = okResult.Value as ImportingResultModel;

            _importManagerMock.VerifyAll();
            Assert.AreEqual(_expectedImportingResult, resultModel);
        }

        #region Helpers
        private ImporterUsageModel CreateImporterUsageModel()
        {
            return new ImporterUsageModel()
            {
                ImporterName = "JSON Importer",
                Parameters = new List<ImporterParameterIntent>()
                {
                    new ImporterParameterIntent()
                    {
                        Name = "Archivo a parsear",
                        Value = "ejemplo.json"
                    }
                }
            };
        }
        private ImportingResult CreateImportingResult()
        {
            ImportingResult result = new ImportingResult();

            result.FailedImportingResorts.Add(new KeyValuePair<Resort, string>(
                new Resort() { Stars = 80 }, "Estrellas invalidas"));

            result.SuccesfulImportedResorts.Add(new Resort() { Id = 3 });
            result.SuccesfulImportedTouristPoints.Add(new TouristPoint() { Id = 23, Region = new Region() { Id = 4 } });

            _expectedImportingResult = new ImportingResultModel(result);

            return result;
        }
        private List<IImporterAdapter> GetImportersList()
        {
            Mock<IImporterAdapter> importer1 = new Mock<IImporterAdapter>();
            Mock<IImporterAdapter> importer2 = new Mock<IImporterAdapter>();

            importer1.Setup(i => i.GetName()).Returns("Importer 1");
            importer1.Setup(i => i.GetNecessaryParameters()).Returns(new List<ImporterParameter>() 
            { 
                new ImporterParameter()
                {
                    Name = "Archivo a parsear",
                    Type = "File"
                },
                new ImporterParameter()
                {
                    Name = "Cantidad maxima a parsear",
                    Type = "Number"
                }
            });
            importer2.Setup(i => i.GetName()).Returns("Importer 2");
            importer2.Setup(i => i.GetNecessaryParameters()).Returns(new List<ImporterParameter>() 
            {
                new ImporterParameter()
                {
                    Name = "String de conexion a la bd",
                    Type = "Text"
                }
            });

            _expectedImporters.Add(new ImporterDetailModel(importer1.Object));
            _expectedImporters.Add(new ImporterDetailModel(importer2.Object));

            return new List<IImporterAdapter>()
            {
                importer1.Object,
                importer2.Object
            };
        }
        #endregion
    }
}
