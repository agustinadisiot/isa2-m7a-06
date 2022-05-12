using System;
using System.Collections.Generic;
using MinTur.BusinessLogic.ResourceManagers;
using MinTur.BusinessLogic.Test.Dummies;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MinTur.BusinessLogicInterface.Pricing;
using MinTur.Domain.Reports;
using System.Linq;

namespace MinTur.BusinessLogic.Test.ResourceManagers
{
    [TestClass]
    public class ReservationManagerTest
    {
        private List<Reservation> _reservations;
        private Mock<IRepositoryFacade> _repositoryFacadeMock;
        private Mock<Reservation> _reservationMock;
        private Mock<IResortPricingCalculator> _resortPricingCalculatorMock;
        private Mock<ReservationReport> _reservationReportMock;
        private Mock<ReservationReportInput> _reservationReportInputMock;
        private ResortPricingCalculatorDummy _resortPricingCalculatorDummy;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _reservations = new List<Reservation>();
            _repositoryFacadeMock = new Mock<IRepositoryFacade>(MockBehavior.Strict);
            _reservationMock = new Mock<Reservation>(MockBehavior.Strict);
            _resortPricingCalculatorMock = new Mock<IResortPricingCalculator>(MockBehavior.Strict);
            _reservationReportMock = new Mock<ReservationReport>(MockBehavior.Strict);
            _reservationReportInputMock = new Mock<ReservationReportInput>(MockBehavior.Strict);
            _resortPricingCalculatorDummy = new ResortPricingCalculatorDummy();

            LoadReservations();
        }

        private void LoadReservations()
        {
            Reservation reservation1 = new Reservation()
            {
                Id = Guid.NewGuid(),
                Name = "Pedro",
                Surname = "Perez"
            };
            Reservation reservation2 = new Reservation() 
            {
                Id = Guid.NewGuid(),
                Name = "Juan",
                Surname = "Fernandez"
            };
            _reservations.Add(reservation1);
            _reservations.Add(reservation2);
        }

        #endregion

        [TestMethod]
        public void GetAllReservationsReturnsAsExpected()
        {
            _repositoryFacadeMock.Setup(r => r.GetAllReservations()).Returns(_reservations);

            ReservationManager reservationManager = new ReservationManager(_repositoryFacadeMock.Object, _resortPricingCalculatorDummy);
            List<Reservation> retrievedReservations = reservationManager.GetAllReservations();

            _repositoryFacadeMock.VerifyAll();
            CollectionAssert.AreEquivalent(retrievedReservations, _reservations);
        }

        [TestMethod]
        public void GetReservationByIdReturnsAsExpected()
        {
            Reservation reservationToFind = new Reservation() { Id = Guid.NewGuid(), Name = "Juan", Surname = "Fernandez" };
            _repositoryFacadeMock.Setup(r => r.GetReservationById(reservationToFind.Id)).Returns(reservationToFind);

            ReservationManager reservationManager = new ReservationManager(_repositoryFacadeMock.Object, _resortPricingCalculatorDummy);
            Reservation retrievedReservation = reservationManager.GetReservationById(reservationToFind.Id);

            _repositoryFacadeMock.VerifyAll();
            Assert.IsTrue(retrievedReservation.Equals(reservationToFind));
        }

        [TestMethod]
        public void RegisterReservationAtResortReturnsAsExpected()
        {
            Guid newReservationId = Guid.NewGuid();
            Reservation createdReservation = CreateReservationWithSpecificId(newReservationId);
            Resort relatedResort = new Resort();

            _reservationMock.Setup(r => r.ValidOrFail(It.IsAny<DateTime>()));
            _reservationMock.SetupGet(r => r.Resort).Returns(new Resort() { Id = 4 });
            _reservationMock.SetupGet(r => r.Accommodation).Returns(new Accommodation());
            _resortPricingCalculatorMock.Setup(c => c.CalculateTotalPriceForAccommodation(relatedResort, _reservationMock.Object.Accommodation))
                .Returns(100);

            _repositoryFacadeMock.Setup(r => r.GetResortById(_reservationMock.Object.Resort.Id)).Returns(relatedResort);
            _repositoryFacadeMock.Setup(r => r.StoreReservation(_reservationMock.Object)).Returns(newReservationId);
            _repositoryFacadeMock.Setup(r => r.GetReservationById(newReservationId)).Returns(createdReservation);

            ReservationManager reservationManager = new ReservationManager(_repositoryFacadeMock.Object, _resortPricingCalculatorMock.Object);
            Reservation retrievedReservation = reservationManager.RegisterReservation(_reservationMock.Object);

            _repositoryFacadeMock.VerifyAll();
            _reservationMock.VerifyAll();
            _resortPricingCalculatorMock.VerifyAll();
            Assert.AreEqual(createdReservation, retrievedReservation);
        }

        [TestMethod]
        public void UpdateReservationStateReturnsAsExpected() 
        {
            Guid reservationToUpdateId = Guid.NewGuid();
            Reservation retrievedReservation = CreateReservationWithSpecificId(reservationToUpdateId);

            Mock<ReservationState> newReservationState = new Mock<ReservationState>(MockBehavior.Strict);
            newReservationState.Setup(r => r.ValidOrFail());

            _repositoryFacadeMock.Setup(r => r.UpdateReservationState(reservationToUpdateId, newReservationState.Object));
            _repositoryFacadeMock.Setup(r => r.GetReservationById(reservationToUpdateId)).Returns(retrievedReservation);

            ReservationManager reservationManager = new ReservationManager(_repositoryFacadeMock.Object, _resortPricingCalculatorDummy);
            Reservation updatedReservation = reservationManager.UpdateReservationState(reservationToUpdateId, newReservationState.Object);

            _repositoryFacadeMock.VerifyAll();
            Assert.AreEqual(updatedReservation, retrievedReservation);
        }

        [TestMethod]
        public void GenerateReservationReportSuccess()
        {
            TouristPoint retrievedTouristPoint = CreateTouristPointWithTwoResortsAndReservations();

            _repositoryFacadeMock.Setup(r => r.GetTouristPointById(It.IsAny<int>())).Returns(retrievedTouristPoint);
            _repositoryFacadeMock.SetupSequence(r => r.GetResortById(It.IsAny<int>())).Returns(retrievedTouristPoint.Resorts.First())
                .Returns(retrievedTouristPoint.Resorts.Last());

            _reservationReportInputMock.Setup(r => r.ValidOrFail());
            _reservationReportInputMock.SetupAllProperties();
            _reservationReportInputMock.Object.InitialDate = DateTime.MinValue;
            _reservationReportInputMock.Object.FinalDate = DateTime.MaxValue;
            _reservationReportInputMock.Object.TouristPointId = retrievedTouristPoint.Id;

            ReservationReport expectedReport = CreateReportByFullProcess(retrievedTouristPoint, _reservationReportInputMock.Object);
            ReservationManager reservationManager = new ReservationManager(_repositoryFacadeMock.Object, _resortPricingCalculatorDummy);
            ReservationReport retrievedReport = reservationManager.GenerateReservationReport(_reservationReportInputMock.Object);

            _repositoryFacadeMock.VerifyAll();
            _reservationReportInputMock.VerifyAll();
            CollectionAssert.AreEqual(expectedReport.ReservationPerResort, retrievedReport.ReservationPerResort);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GenerateReservationReportFails()
        {
            TouristPoint retrievedTouristPoint = CreateTouristPointWithTwoResortsAndNoReservations();

            _repositoryFacadeMock.Setup(r => r.GetTouristPointById(It.IsAny<int>())).Returns(retrievedTouristPoint);
            _repositoryFacadeMock.SetupSequence(r => r.GetResortById(It.IsAny<int>())).Returns(retrievedTouristPoint.Resorts.First())
                .Returns(retrievedTouristPoint.Resorts.Last());
            _reservationReportInputMock.Setup(r => r.ValidOrFail());

            ReservationManager reservationManager = new ReservationManager(_repositoryFacadeMock.Object, _resortPricingCalculatorDummy);
            reservationManager.GenerateReservationReport(_reservationReportInputMock.Object);

            _repositoryFacadeMock.VerifyAll();
            _reservationReportInputMock.VerifyAll();
        }

        #region Helpers
        public ReservationReport CreateReportByFullProcess(TouristPoint touristPoint, ReservationReportInput input)
        {
            ReservationReport report = new ReservationReport()
            {
                Input = input
            };

            foreach(Resort resort in touristPoint.Resorts)
            {
                report.AddReportEntry(resort);
            }

            report.RemoveEntriesWithNoReservations();
            report.SortReportEntries();

            return report;
        }
        public TouristPoint CreateTouristPointWithTwoResortsAndNoReservations()
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 4,
                Name = "Punta del este",
            };
            touristPoint.Resorts.Add(new Resort() { Id = 4, Name = "Grand Hotel" });
            touristPoint.Resorts.Add(new Resort() { Id = 7, Name = "Hotel Argentino" });

            return touristPoint;
        }
        public TouristPoint CreateTouristPointWithTwoResortsAndReservations()
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 4,
                Name = "Punta del este",
            };
            Resort resort1 = new Resort()
            {
                Id = 4,
                Name = "Grand Hotel",
                TouristPointId = 4,
                Reservations = new List<Reservation>()
                {
                    new Reservation()
                    {
                        Accommodation = new Accommodation()
                        {
                            CheckIn = new DateTime(2020,10,10),
                            CheckOut = new DateTime(2020,11,13)
                        }
                    }
                }
            };
            Resort resort2 = new Resort()
            {
                Id = 4,
                Name = "Small Hotel",
                TouristPointId = 4
            };

            touristPoint.Resorts.Add(resort1);
            touristPoint.Resorts.Add(resort2);

            return touristPoint;
        }
        public Reservation CreateReservationWithSpecificId(Guid reservationId) 
        {
            Reservation reservation =  new Reservation()
            {
                Id = reservationId,
                Name = "Pedro",
                Surname = "Perez",
                Email = "pedro@perez.com",
                Accommodation = new Accommodation()
                {
                    Id = 2,
                    CheckIn = new DateTime(2020, 10, 3),
                    CheckOut = new DateTime(2020, 10, 8)
                },
                Resort = new Resort()
                {
                    Id = 2,
                    Name = "Hotel italiano"
                }
            };
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Adult.ToString(), Amount = 2 });
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Kid.ToString(), Amount = 1 });

            return reservation;
        }
        #endregion
    }
}