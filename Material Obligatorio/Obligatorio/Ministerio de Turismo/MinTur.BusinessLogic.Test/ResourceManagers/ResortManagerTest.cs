using MinTur.BusinessLogic.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System;
using MinTur.Domain.SearchCriteria;

namespace MinTur.BusinessLogic.Test.ResourceManagers
{
    [TestClass]
    public class ResortManagerTest
    {
        private List<Resort> _resorts;
        private Mock<IRepositoryFacade> _repositoryFacadeMock;
        private Mock<Accommodation> _accomodationMock;
        private Mock<Resort> _resortMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _resorts = new List<Resort>();
            _repositoryFacadeMock = new Mock<IRepositoryFacade>(MockBehavior.Strict);
            _accomodationMock = new Mock<Accommodation>(MockBehavior.Strict);
            _resortMock = new Mock<Resort>(MockBehavior.Strict);

            LoadResorts();
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

        #endregion

        [TestMethod]
        public void RegisterResortReturnsAsExpected()
        {
            int resortId = 6;
            Resort createdResort = GetResortByIdHelper(resortId);
            TouristPoint touristPoint = CreateTouristPointWithSpecificId(1);

            _resortMock.Setup(r => r.ValidOrFail());
            _repositoryFacadeMock.Setup(r => r.GetTouristPointById(It.IsAny<int>())).Returns(touristPoint);
            _repositoryFacadeMock.Setup(r => r.StoreResort(_resortMock.Object)).Returns(resortId);
            _repositoryFacadeMock.Setup(r => r.GetResortById(resortId)).Returns(createdResort);

            ResortManager resortManager = new ResortManager(_repositoryFacadeMock.Object);
            Resort retrievedResort = resortManager.RegisterResort(_resortMock.Object);

            _repositoryFacadeMock.VerifyAll();
            _resortMock.VerifyAll();
            Assert.AreEqual(createdResort, retrievedResort);
        }

        [TestMethod]
        public void GetAllResortsForAccommodationByMatchingCriteriaReturnsAsExpected()
        {
            ResortSearchCriteria resortSearchCriteria = new ResortSearchCriteria();
            _accomodationMock.Setup(a => a.ValidOrFail(DateTime.Today)).Returns(true);
            _repositoryFacadeMock.Setup(r => r.GetAllResortsByMatchingCriteria(resortSearchCriteria.MatchesCriteria)).Returns(_resorts);

            ResortManager resortManager = new ResortManager(_repositoryFacadeMock.Object);
            List<Resort> retrievedResorts = resortManager.GetAllResortsForAccommodationByMatchingCriteria(_accomodationMock.Object, resortSearchCriteria);

            _accomodationMock.VerifyAll();
            _repositoryFacadeMock.VerifyAll();
            CollectionAssert.AreEquivalent(retrievedResorts, _resorts);
        }

        [TestMethod]
        public void GetAllResortsByMatchingCriteriaReturnsAsExpected()
        {
            ResortSearchCriteria resortSearchCriteria = new ResortSearchCriteria();
            _repositoryFacadeMock.Setup(r => r.GetAllResortsByMatchingCriteria(resortSearchCriteria.MatchesCriteria)).Returns(_resorts);

            ResortManager resortManager = new ResortManager(_repositoryFacadeMock.Object);
            List<Resort> retrievedResorts = resortManager.GetAllResortsByMatchingCriteria(resortSearchCriteria);

            _repositoryFacadeMock.VerifyAll();
            CollectionAssert.AreEquivalent(retrievedResorts, _resorts);
        }

        [TestMethod]
        public void GetResortByIdReturnsAsExpected()
        {
            int resortId = 6;
            Resort expectedResort = GetResortByIdHelper(resortId);

            _repositoryFacadeMock.Setup(r => r.GetResortById(resortId)).Returns(expectedResort);
            ResortManager resortManager = new ResortManager(_repositoryFacadeMock.Object);

            Resort retrievedResort = resortManager.GetResortById(resortId);
            _repositoryFacadeMock.VerifyAll();
            Assert.IsTrue(retrievedResort.Equals(expectedResort));
        }

        [TestMethod]
        public void UpdateResortAvailabilityReturnsAsExpected()
        {
            int resortId = 6;
            bool newAvailability = true;
            Resort resortToUpdate = GetResortByIdHelper(resortId);

            _repositoryFacadeMock.Setup(r => r.GetResortById(resortId)).Returns(resortToUpdate);
            resortToUpdate.Available = newAvailability;

            _repositoryFacadeMock.Setup(r => r.UpdateResort(resortToUpdate));
            ResortManager resortManager = new ResortManager(_repositoryFacadeMock.Object);

            Resort updatedResort = resortManager.UpdateResortAvailability(resortId, newAvailability);
            _repositoryFacadeMock.VerifyAll();
            Assert.AreEqual(resortToUpdate, updatedResort);
        }
        public void DeleteResortReturnsAsExpected()
        {
            int resortId = 6;
            Resort resortToBeDeleted = GetResortByIdHelper(resortId);

            _repositoryFacadeMock.Setup(r => r.GetResortById(resortId)).Returns(resortToBeDeleted);
            _repositoryFacadeMock.Setup(r => r.DeleteResort(resortToBeDeleted));
            ResortManager resortManager = new ResortManager(_repositoryFacadeMock.Object);

            resortManager.DeleteResortById(resortId);
            _repositoryFacadeMock.VerifyAll();
            Assert.IsTrue(resortId.Equals(resortToBeDeleted.Id));
        }

        #region Helpers
        private Resort GetResortByIdHelper(int resortId)
        {
            Resort resort = new Resort()
            {
                Id = resortId,
                Name = "Hotel Italiano",
                Address = "Una calle en uruguay",
                Description = "El hotel mas lindo de todo uruguay",
                PhoneNumber = "095867458",
                PricePerNight = 100,
                ReservationMessage = "Gracias por confiar en nosotros, por mas info llamar al num de contacto",
                Stars = 4,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Cabo Polonio"
                },
                TouristPointId = 2
            };
            resort.Images.Add(new Image() { Id = 2, Data = "jfiewhgwe" });
            resort.Images.Add(new Image() { Id = 7, Data = "ghjtruihjtriojh" });

            return resort;
        }

        public TouristPoint CreateTouristPointWithSpecificId(int touristPointId)
        {
            return new TouristPoint()
            {
                Id = touristPointId,
                Name = "Punta del Este",
                Region = new Region()
                {
                    Id = 3,
                    Name = "Metropolitana"
                },
                Description = "Descripcion...",
                RegionId = 3,
                Image = new Image() { Id = 3, Data = "jsafigrehgeruhwdjiw" }
            };
        }

        #endregion

    }
}
