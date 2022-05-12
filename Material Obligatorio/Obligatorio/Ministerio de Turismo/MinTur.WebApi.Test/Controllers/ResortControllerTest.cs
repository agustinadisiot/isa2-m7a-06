using System.Collections.Generic;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;
using MinTur.Models.Out;
using Moq;
using MinTur.WebApi.Controllers;
using MinTur.Domain.SearchCriteria;
using MinTur.BusinessLogicInterface.Pricing;
using MinTur.WebApi.Test.Dummies;
using System.Linq;

namespace MinTur.WebApi.Test.Controllers
{
    [TestClass]
    public class ResortControllerTest
    {
        private List<Resort> _resorts;
        private List<ResortDetailsModel> _resortsDetailsModels;
        private List<ResortSearchResultModel> _resortClientSearchModels;
        private Mock<IResortManager> _resortManagerMock;
        private Mock<IResortPricingCalculator> _resortPricingCalculatorMock;
        private ResortPricingCalculatorDummy _resortPricingCalculatorDummy;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _resorts = new List<Resort>();
            _resortsDetailsModels = new List<ResortDetailsModel>();
            _resortClientSearchModels = new List<ResortSearchResultModel>();
            _resortManagerMock = new Mock<IResortManager>(MockBehavior.Strict);
            _resortPricingCalculatorMock = new Mock<IResortPricingCalculator>(MockBehavior.Strict);
            _resortPricingCalculatorDummy = new ResortPricingCalculatorDummy();

            LoadResorts();
            LoadResortsModels();
        }

        private void LoadResorts()
        {
            Resort resort1 = new Resort()
            {
                Id = 3,
                Name = "Hotel Italiano",
                Description = "Hotel lindo con estilo italinao",
                Address = "Calle en PDE",
                PhoneNumber = "1849848",
                PricePerNight = 100,
                Stars = 4,
                ReservationMessage = "Gracias por quere venir :)",
                TouristPointId = 2,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del Este"
                }
            };
            Resort resort2 = new Resort()
            {
                Id = 4,
                Name = "Hotel argentino",
                Description = "Buen hotel",
                Address = "Calle en Piriapolis",
                PhoneNumber = "18432239848",
                PricePerNight = 400,
                Stars = 5,
                ReservationMessage = "Gracias por quere venir amigos",
                TouristPointId = 3,
                TouristPoint = new TouristPoint()
                {
                    Id = 3,
                    Name = "Piriapolis"
                }
            };
            _resorts.Add(resort1);
            _resorts.Add(resort2);
        }


        private void LoadResortsModels()
        {
            foreach (Resort resort in _resorts)
            {
                _resortsDetailsModels.Add(new ResortDetailsModel(resort));
                _resortClientSearchModels.Add(new ResortSearchResultModel(resort));
            }
        }

        #endregion

        [TestMethod]
        public void GetAllResortsClientSearchOkTest()
        {

            _resortManagerMock.Setup(c => c.GetAllResortsForAccommodationByMatchingCriteria(It.IsAny<Accommodation>(), It.IsAny<ResortSearchCriteria>())).Returns(_resorts);
            _resortPricingCalculatorMock.Setup(r => r.CalculateTotalPriceForAccommodation(It.IsAny<Resort>(), It.IsAny<Accommodation>())).Returns(100);
            ResortController resortController = new ResortController(_resortManagerMock.Object, _resortPricingCalculatorMock.Object);

            IActionResult result = resortController.GetAll(new ResortSearchModel() { ClientSearch = true }, new AccommodationIntentModel());
            OkObjectResult okResult = result as OkObjectResult;
            List<ResortSearchResultModel> responseModel = okResult.Value as List<ResortSearchResultModel>;

            _resortManagerMock.VerifyAll();
            _resortPricingCalculatorMock.VerifyAll();
            CollectionAssert.AreEquivalent(responseModel, _resortClientSearchModels);
            Assert.IsTrue(responseModel.All(m => m.TotalPrice == 100));
        }

        [TestMethod]
        public void GetAllResortsNotClientSearchOkTest()
        {

            _resortManagerMock.Setup(c => c.GetAllResortsByMatchingCriteria(It.IsAny<ResortSearchCriteria>())).Returns(_resorts);
            ResortController resortController = new ResortController(_resortManagerMock.Object, _resortPricingCalculatorDummy);

            IActionResult result = resortController.GetAll(new ResortSearchModel() { ClientSearch = false }, new AccommodationIntentModel());
            OkObjectResult okResult = result as OkObjectResult;
            List<ResortDetailsModel> responseModel = okResult.Value as List<ResortDetailsModel>;

            _resortManagerMock.VerifyAll();
            CollectionAssert.AreEquivalent(responseModel, _resortsDetailsModels);
        }

        [TestMethod]
        public void GetSpecificResortOkTest()
        {
            Resort expectedResort = CreateResort();

            _resortManagerMock.Setup(r => r.GetResortById(expectedResort.Id)).Returns(expectedResort);
            ResortController resortController = new ResortController(_resortManagerMock.Object, _resortPricingCalculatorDummy);

            IActionResult result = resortController.GetSpecificResort(expectedResort.Id);
            OkObjectResult okResult = result as OkObjectResult;

            _resortManagerMock.VerifyAll();
            Assert.IsTrue(okResult.StatusCode == StatusCodes.Status200OK);
            Assert.IsTrue(okResult.Value.Equals(new ResortDetailsModel(expectedResort)));
        }

        [TestMethod]
        public void UpdateResortAvailabilityCreatedTest()
        {
            ResortPartialUpdateModel resortUpdateModel = CreateResortUpdateModel();
            Resort updatedResort = CreateResort();
            updatedResort.Available = resortUpdateModel.GetNewAvailabilty();

            _resortManagerMock.Setup(r => r.UpdateResortAvailability(It.IsAny<int>(), It.IsAny<bool>())).Returns(updatedResort);
            ResortController resortController = new ResortController(_resortManagerMock.Object, _resortPricingCalculatorDummy);

            IActionResult result = resortController.UpdateResortAvailability(updatedResort.Id, resortUpdateModel);
            OkObjectResult okResult = result as OkObjectResult;

            _resortManagerMock.VerifyAll();
            Assert.IsTrue(okResult.StatusCode == StatusCodes.Status200OK);
            Assert.IsTrue(okResult.Value.Equals(new ResortDetailsModel(updatedResort)));
        }

        [TestMethod]
        public void CreateNewResortTest()
        {
            ResortIntentModel resortIntentModel = CreateResortIntentModel();
            Resort createdResort = CreateResort();

            _resortManagerMock.Setup(r => r.RegisterResort(It.IsAny<Resort>())).Returns(createdResort);
            ResortController resortController = new ResortController(_resortManagerMock.Object, _resortPricingCalculatorDummy);

            IActionResult result = resortController.CreateResort(resortIntentModel);
            CreatedResult createdResult = result as CreatedResult;

            _resortManagerMock.VerifyAll();
            Assert.IsTrue(createdResult.StatusCode == StatusCodes.Status201Created);
            Assert.IsTrue(createdResult.Value.Equals(new ResortDetailsModel(createdResort)));
        }

        [TestMethod]
        public void DeleteResortTest()
        {
            Resort resortToDelete = CreateResort();
            int resortIdToDelete = resortToDelete.Id;
            string succesfulDeletitionMessage = new { ResultMessage = $"Resort {resortIdToDelete} succesfuly deleted" }.ToString();

            _resortManagerMock.Setup(r => r.DeleteResortById(resortIdToDelete));
            ResortController resortController = new ResortController(_resortManagerMock.Object, _resortPricingCalculatorDummy);

            IActionResult result = resortController.DeleteResort(resortIdToDelete);
            OkObjectResult okResult = result as OkObjectResult;
            string retrievedResultMessage = okResult.Value.ToString();

            _resortManagerMock.VerifyAll();
            Assert.AreEqual(succesfulDeletitionMessage, retrievedResultMessage);
        }

        #region Helpers
        private Reservation GetCreatedReservation()
        {
            return new Reservation()
            {
                Resort = new Resort()
                {
                    PhoneNumber = "095784685",
                    ReservationMessage = "Thanks for choosing us ..."
                }
            };
        }
        private Resort CreateResort()
        {
            return new Resort()
            {
                Id = 3,
                Name = "Hotel Italiano",
                Description = "Hotel lindo con estilo italinao",
                Address = "Calle en PDE",
                PhoneNumber = "1849848",
                PricePerNight = 100,
                Stars = 4,
                ReservationMessage = "Gracias por quere venir :)",
                TouristPointId = 2,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del Este"
                }
            };
        }

        public ResortPartialUpdateModel CreateResortUpdateModel()
        {
            return new ResortPartialUpdateModel()
            {
                NewAvailability = true
            };
        }
        private ResortIntentModel CreateResortIntentModel()
        {
            return new ResortIntentModel()
            {
                Name = "Hotel Italiano",
                Description = "Hotel lindo con estilo italinao",
                Address = "Calle en PDE",
                PhoneNumber = "1849848",
                PricePerNight = 100,
                Stars = 4,
                ReservationMessage = "Gracias por quere venir :)",
                TouristPointId = 2,
                Available = true
            };
        }

        #endregion

    }
}
