using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.BusinessLogic.Importing;
using MinTur.BusinessLogic.Test.Stubs;
using MinTur.BusinessLogicInterface.Importing;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.Importing;
using MinTur.Exceptions;
using MinTur.ImporterInterface.DTOs;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinTur.BusinessLogic.Test.Importing
{
    [TestClass]
    public class ImporterManagerTest
    {
        Mock<IConfiguration> _configurationMock;
        Mock<IRepositoryFacade> _repositoryFacadeMock;
        ImportingDTOMapper _mapper;
        ImporterStub _importerStub;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _importerStub = new ImporterStub();
            _repositoryFacadeMock = new Mock<IRepositoryFacade>(MockBehavior.Strict);
            string currentDirectory = Directory.GetCurrentDirectory();

            _configurationMock = new Mock<IConfiguration>(MockBehavior.Strict);
            _configurationMock.Setup(c => c.GetSection(@"ImportersDllDirectory").Value)
                .Returns(currentDirectory);

            _mapper = new ImportingDTOMapper();
        }
        #endregion

        [TestMethod]
        public void GetImportersReturnsAsExpected()
        {
            List<IImporterAdapter> expectedImporters = new List<IImporterAdapter>() { new ImporterAdapter(_importerStub) };

            ImporterManager importerManager = new ImporterManager(_configurationMock.Object, _repositoryFacadeMock.Object);
            List<IImporterAdapter> retrievedImporters = importerManager.GetImporters();

            CollectionAssert.AreEquivalent(expectedImporters.Select(i => i.GetType()).ToList(), 
                retrievedImporters.Select(i => i.GetType()).ToList());
        }

        [TestMethod]
        public void ImportResourcesDoesAsExpected()
        {
            ImportingInput importingInput = CreateImportingInput();
            ImportingResult expectedResult = GetExpectedImportingResult();
            SetupRepositoryMockAccordingToImportingResult();

            ImporterManager importerManager = new ImporterManager(_configurationMock.Object, _repositoryFacadeMock.Object);
            ImportingResult importingResult = importerManager.ImportResources(importingInput);

            _repositoryFacadeMock.VerifyAll();
            Assert.AreEqual(expectedResult, importingResult);
        }

        #region Helpers
        public ImportingInput CreateImportingInput()
        {
            return new ImportingInput()
            {
                ImporterName = _importerStub.GetName(),
                Parameters = new List<ImporterParameterInput>()
                {
                    new ImporterParameterInput()
                    {
                        Name = "Archivo a parsear",
                        Value = "recursos.sth"
                    }
                }
            };
        }
        public ImportingResult GetExpectedImportingResult()
        {
            List<ImportedResort> importedResorts = _importerStub.RetrieveResorts(new List<ImportingParameterValue>());
            TouristPoint storedTouristPoint = _mapper.MapTouristPoint(importedResorts.ElementAt(2).TouristPoint);
            Resort storedResort = _mapper.MapResort(importedResorts.ElementAt(2));
            ImportingResult importingResult = new ImportingResult();

            storedResort.Id = 20;
            storedTouristPoint.Id = 12;

            importingResult.FailedImportingResorts.Add(new KeyValuePair<Resort, string>(
                _mapper.MapResort(importedResorts.ElementAt(0)), "Invalid stars value - must be between 1 and 5"));
            importingResult.FailedImportingResorts.Add(new KeyValuePair<Resort, string>(
                _mapper.MapResort(importedResorts.ElementAt(1)), "Could not find specified region"));
            importingResult.SuccesfulImportedResorts.Add(storedResort);
            importingResult.SuccesfulImportedTouristPoints.Add(storedTouristPoint);

            return importingResult;
        }
        public void SetupRepositoryMockAccordingToImportingResult()
        {
            List<ImportedResort> importedResorts = _importerStub.RetrieveResorts(new List<ImportingParameterValue>());
            TouristPoint storedTouristPoint = _mapper.MapTouristPoint(importedResorts.ElementAt(2).TouristPoint);
            Resort storedResort = _mapper.MapResort(importedResorts.ElementAt(2));

            storedResort.Id = 20;
            storedTouristPoint.Id = 12;

            _repositoryFacadeMock.SetupSequence(r => r.GetTouristPointById(It.IsAny<int>()))
                .Throws(new ResourceNotFoundException(""))
                .Throws(new ResourceNotFoundException(""))
                .Returns(storedTouristPoint);
            _repositoryFacadeMock.SetupSequence(r => r.StoreTouristPoint(It.IsAny<TouristPoint>()))
                .Throws(new ResourceNotFoundException("Could not find specified region"))
                .Returns(storedTouristPoint.Id);
            _repositoryFacadeMock.Setup(r => r.StoreResort(It.IsAny<Resort>())).Returns(storedResort.Id);
            _repositoryFacadeMock.Setup(r => r.GetResortById(It.IsAny<int>())).Returns(storedResort);
        }
        #endregion


    }
}
