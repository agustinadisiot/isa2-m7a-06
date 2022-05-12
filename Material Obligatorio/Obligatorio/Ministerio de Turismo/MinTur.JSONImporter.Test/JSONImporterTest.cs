using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Exceptions;
using MinTur.ImporterInterface.DTOs;
using Moq;
using System.Collections.Generic;
using System.IO;

namespace MinTur.JSONImporter.Test
{
    [TestClass]
    public class JSONImporterTest
    {
        Mock<IConfiguration> _configurationMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            _configurationMock = new Mock<IConfiguration>(MockBehavior.Strict);
            _configurationMock.Setup(c => c.GetSection("JSONFilesForImporter").Value)
                .Returns(projectPath + "/JSONFiles");
        }
        #endregion

        [TestMethod]
        public void GetNameReturnsAsExpected()
        {
            string expectedName = "JSON Importer";
            JSONImporter jsonImporter = new JSONImporter(_configurationMock.Object);

            string retrievedName = jsonImporter.GetName();
            Assert.AreEqual(expectedName, retrievedName);
        }

        [TestMethod]
        public void GetParametersReturnsAsExpected()
        {
            List<ImporterParameterDescription> expectedParameters = new List<ImporterParameterDescription>()
            {
                new ImporterParameterDescription()
                {
                    Name = "File to parse",
                    Type = PossibleParameters.File
                }
            };
            JSONImporter jsonImporter = new JSONImporter(_configurationMock.Object);

            List<ImporterParameterDescription> retrievedParameters = jsonImporter.GetNecessaryParameters();
            CollectionAssert.AreEquivalent(expectedParameters, retrievedParameters);
        }

        [TestMethod]
        public void RetrieveResortsFromCorrectlyFromedJSON1()
        {
            List<ImportedResort> expectedResorts = GetCorrectlyFormedJSON1ExpectedResult();
            List<ImportingParameterValue> parameters = new List<ImportingParameterValue>()
            {
                new ImportingParameterValue()
                {
                    Name = "File to parse",
                    Value = "CorrectlyFormedJSON1.json"
                }
            };
            JSONImporter jsonImporter = new JSONImporter(_configurationMock.Object);

            List<ImportedResort> retrievedResorts = jsonImporter.RetrieveResorts(parameters);
            CollectionAssert.AreEqual(expectedResorts, retrievedResorts);
        }

        [TestMethod]
        public void RetrieveResortsFromCorrectlyFromedJSON2()
        {
            List<ImportedResort> expectedResorts = GetCorrectlyFormedJSON2ExpectedResult();
            List<ImportingParameterValue> parameters = new List<ImportingParameterValue>()
            {
                new ImportingParameterValue()
                {
                    Name = "File to parse",
                    Value = "CorrectlyFormedJSON2.json"
                }
            };
            JSONImporter jsonImporter = new JSONImporter(_configurationMock.Object);

            List<ImportedResort> retrievedResorts = jsonImporter.RetrieveResorts(parameters);
            CollectionAssert.AreEqual(expectedResorts, retrievedResorts);
        }

        [TestMethod]
        public void RetrieveResortsWronglyFormedJSON()
        {
            List<ImportedResort> expectedResorts = GetWronglyFormedJSONExpectedResult();
            List<ImportingParameterValue> parameters = new List<ImportingParameterValue>()
            {
                new ImportingParameterValue()
                {
                    Name = "File to parse",
                    Value = "WronglyFormedJSON.json"
                }
            };
            JSONImporter jsonImporter = new JSONImporter(_configurationMock.Object);

            List<ImportedResort> retrievedResorts = jsonImporter.RetrieveResorts(parameters);
            CollectionAssert.AreEqual(expectedResorts, retrievedResorts);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void RetrieveResortsWrongNumberOfParameters()
        {
            List<ImportingParameterValue> parameters = new List<ImportingParameterValue>();
            JSONImporter jsonImporter = new JSONImporter(_configurationMock.Object);

            jsonImporter.RetrieveResorts(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void RetrieveResortsWrongParameters()
        {
            List<ImportingParameterValue> parameters = new List<ImportingParameterValue>() 
            {
                new ImportingParameterValue()
                {
                    Name = "Sth",
                    Value = "value"
                }
            };
            JSONImporter jsonImporter = new JSONImporter(_configurationMock.Object);

            jsonImporter.RetrieveResorts(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void RetrieveResortsNonExistentFile()
        {
            List<ImportingParameterValue> parameters = new List<ImportingParameterValue>()
            {
                new ImportingParameterValue()
                {
                    Name = "File to parse",
                    Value = "DoesntExist.json"
                }
            };
            JSONImporter jsonImporter = new JSONImporter(_configurationMock.Object);

            jsonImporter.RetrieveResorts(parameters);
        }


        #region Helpers
        private List<ImportedResort> GetCorrectlyFormedJSON1ExpectedResult()
        {
            return new List<ImportedResort>()
            {
                CreateItalianResort(),
                CreateGermanResort()
            };
        }
        private List<ImportedResort> GetCorrectlyFormedJSON2ExpectedResult()
        {
            return new List<ImportedResort>()
            {
                CreateItalianResort()
            };
        }
        private List<ImportedResort> GetWronglyFormedJSONExpectedResult()
        {
            return new List<ImportedResort>()
            {
                new ImportedResort()
            };
        }
        private ImportedResort CreateItalianResort()
        {
            return new ImportedResort()
            {
                Name = "Hotel Italiano",
                Stars = 3,
                Address = "Rambla 1234",
                PhoneNumber = "0964785",
                ReservationMessage = "Gracias por tu reserva",
                Description = "El Hotel Italiano es el mejor",
                ImagesData = new List<string>()
                    {
                        "imagen1",
                        "imagen2"
                    },
                Available = true,
                PricePerNight = 100,
                TouristPoint = new ImportedTouristPoint()
                {
                    Id = 2,
                    Name = "Punta del Este",
                    Description = "Lugar lindo en uru",
                    Image = "imagen de punta",
                    RegionId = 3,
                    CategoriesId = new List<int>()
                        {
                            2,
                            3,
                            4
                        }
                }
            };
        }
        private ImportedResort CreateGermanResort()
        {
            return new ImportedResort()
            {
                Name = "Hotel Aleman",
                Stars = 3,
                Address = "Av Italia 1234",
                PhoneNumber = "0945231",
                ReservationMessage = "Gracias por tu reserva",
                Description = "El Hotel Aleman es el mejor de los mejores",
                ImagesData = new List<string>()
                {
                    "imagen1"
                },
                Available = false,
                PricePerNight = 130,
                TouristPoint = new ImportedTouristPoint()
                {
                    Id = 2,
                    Name = "Montevideo",
                    Description = "La capital",
                    Image = "imagen de mvdeo",
                    RegionId = 1,
                    CategoriesId = new List<int>()
                    {
                        4
                    }
                }
            };
        }
        #endregion
    
    }
}
