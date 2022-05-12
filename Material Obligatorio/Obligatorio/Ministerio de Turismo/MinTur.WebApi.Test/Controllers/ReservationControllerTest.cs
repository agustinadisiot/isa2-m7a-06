using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;
using MinTur.Models.Out;
using Moq;
using System;
using System.Collections.Generic;
using MinTur.WebApi.Controllers;
using MinTur.Domain.Reports;
using System.Linq;

namespace MinTur.WebApi.Test.Controllers
{
    [TestClass]
    public class ReservationControllerTest
    {
        private List<Reservation> _reservations;
        private List<ReservationDetailsModel> _reservationDetailsModels;
        private Mock<IReservationManager> _reservationManagerMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _reservations = new List<Reservation>();
            _reservationDetailsModels = new List<ReservationDetailsModel>();
            _reservationManagerMock = new Mock<IReservationManager>(MockBehavior.Strict);

            LoadReservations();
            LoadReservationModels();
        }

        private void LoadReservations()
        {
            Reservation reservation1 = CreateReservationWithSpecificId(Guid.NewGuid());
            Reservation reservation2 = CreateReservationWithSpecificId(Guid.NewGuid());
            _reservations.Add(reservation1);
            _reservations.Add(reservation2);
        }


        private void LoadReservationModels()
        {
            foreach (Reservation reservation in _reservations)
            {
                _reservationDetailsModels.Add(new ReservationDetailsModel(reservation));
            }
        }

        #endregion

        [TestMethod]
        public void GetAllReservationsOkTest()
        {
            _reservationManagerMock.Setup(c => c.GetAllReservations()).Returns(_reservations);
            ReservationController reservationController = new ReservationController(_reservationManagerMock.Object);

            IActionResult result = reservationController.GetAll();
            OkObjectResult okResult = result as OkObjectResult;
            List<ReservationDetailsModel> responseModel = okResult.Value as List<ReservationDetailsModel>;

            _reservationManagerMock.VerifyAll();
            CollectionAssert.AreEquivalent(responseModel, _reservationDetailsModels);
        }

        [TestMethod]
        public void GetSpecificReservationOkTest() 
        {
            Guid reservationId = Guid.NewGuid();
            Reservation expectedReservation = CreateReservationWithSpecificId(reservationId);

            _reservationManagerMock.Setup(c => c.GetReservationById(reservationId)).Returns(expectedReservation);
            ReservationController reservationController = new ReservationController(_reservationManagerMock.Object);

            IActionResult result = reservationController.GetSpecificReservation(reservationId);
            OkObjectResult okResult = result as OkObjectResult;
            ReservationDetailsModel responseModel = okResult.Value as ReservationDetailsModel;

            Assert.AreEqual(new ReservationDetailsModel(expectedReservation), responseModel);
        }

        [TestMethod]
        public void GetReservationStateOKTest()
        {
            Reservation reservation = new Reservation() { Id = Guid.NewGuid(), Name = "Pedro", Surname = "Perez" };

            _reservationManagerMock.Setup(c => c.GetReservationById(It.IsAny<Guid>())).Returns(reservation);
            ReservationController reservationController = new ReservationController(_reservationManagerMock.Object);

            IActionResult result = reservationController.GetReservationState(reservation.Id);
            OkObjectResult okResult = result as OkObjectResult;
            ReservationCheckStateModel responseModel = okResult.Value as ReservationCheckStateModel;

            _reservationManagerMock.VerifyAll();
            Assert.IsTrue(responseModel.Equals(new ReservationCheckStateModel(reservation)));
        }

        [TestMethod]
        public void MakeReservationCreatedTest()
        {
            ReservationIntentModel reservationIntentModel = CreateReservationIntentModel();
            Reservation createdReservation = CreateReservationWithSpecificId(Guid.NewGuid());

            _reservationManagerMock.Setup(r => r.RegisterReservation(It.IsAny<Reservation>())).Returns(createdReservation);
            ReservationController reservationController = new ReservationController(_reservationManagerMock.Object);

            IActionResult result = reservationController.MakeReservation(reservationIntentModel);
            CreatedResult createdResult = result as CreatedResult;

            _reservationManagerMock.VerifyAll();
            Assert.IsTrue(createdResult.Value.Equals(new ReservationConfirmationModel(createdReservation)));
        }

        [TestMethod]
        public void UpdateReservationStateOkTest() 
        {
            Guid reservationToUpdateId = Guid.NewGuid();
            ReservationStateIntentModel reservationStateIntentModel = new ReservationStateIntentModel()
            {
                State = "Denied",
                Description = "Sorry, your bank rejected the payment"
            };
            Reservation updatedReservation = CreateReservationWithSpecificId(reservationToUpdateId);
            updatedReservation.UpdateState(reservationStateIntentModel.ToEntity());
            updatedReservation.Id = reservationToUpdateId;

            _reservationManagerMock.Setup(r => r.UpdateReservationState(reservationToUpdateId, reservationStateIntentModel.ToEntity())).Returns(updatedReservation);
            ReservationController reservationController = new ReservationController(_reservationManagerMock.Object);

            IActionResult result = reservationController.UpdateReservationState(reservationToUpdateId, reservationStateIntentModel);
            OkObjectResult okResult = result as OkObjectResult;
            ReservationCheckStateModel responseModel = okResult.Value as ReservationCheckStateModel;

            _reservationManagerMock.VerifyAll();
            Assert.AreEqual(new ReservationCheckStateModel(updatedReservation), responseModel);
        }

        [TestMethod]
        public void GenerateReportOkTest()
        {
            ReservationReportInputModel inputModel = new ReservationReportInputModel()
            {
                TouristPointId = 2,
                InitialDate = new DateTime(2020, 10, 10),
                FinalDate = new DateTime(2020, 11, 13)
            };
            ReservationReport generatedReport = GenerateReport();

            _reservationManagerMock.Setup(r => r.GenerateReservationReport(It.IsAny<ReservationReportInput>())).Returns(generatedReport);
            ReservationController reservationController = new ReservationController(_reservationManagerMock.Object);
            
            IActionResult result = reservationController.GenerateReport(inputModel);
            OkObjectResult okResult = result as OkObjectResult;
            List<ReservationIndividualReportModel> retrievedReport = okResult.Value as List<ReservationIndividualReportModel>;

            List<ReservationIndividualReportModel> expectedReport = generatedReport.ReservationPerResort
                .Select(r => new ReservationIndividualReportModel(r)).ToList();

            _reservationManagerMock.VerifyAll();
            CollectionAssert.AreEquivalent(expectedReport, retrievedReport);
        }

        #region Helpers
        private ReservationReport GenerateReport()
        {
            ReservationReport report = new ReservationReport();

            report.ReservationPerResort.Add(new KeyValuePair<Resort, int>(new Resort() { Name = "Hotel Italiano" }, 2));
            report.ReservationPerResort.Add(new KeyValuePair<Resort, int>(new Resort() { Name = "Hotel Aleman" }, 2));

            return report;
        }
        private ReservationIntentModel CreateReservationIntentModel()
        {
            return new ReservationIntentModel()
            {
                ResortId = 4,
                Name = "Pedro",
                Surname = "Perez",
                Email = "pedroPerez@gmail.com",
                CheckIn = new DateTime(2020, 9, 28),
                CheckOut = new DateTime(2020, 10, 7),
                AdultsAmount = 2,
                KidsAmount = 3,
                BabiesAmount = 0
            };
        }
        private Reservation CreateReservationWithSpecificId(Guid reservationId)
        {
            Reservation reservation = new Reservation()
            {
                Id = reservationId,
                Name = "Pedro",
                Surname = "Perez",
                Resort = new Resort()
                {
                    Id = 3,
                    PhoneNumber = "095784685",
                    ReservationMessage = "Thanks for choosing us ..."
                },
                Email = "pedro@perez.com",
                TotalPrice = 600,
                Accommodation = new Accommodation() 
                {
                    CheckIn = new DateTime(2020,10,10),
                    CheckOut = new DateTime(2020,10,20)
                }
            };
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Adult.ToString(), Amount = 3 });
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Baby.ToString(), Amount = 1 });
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Kid.ToString(), Amount = 2 });
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Retired.ToString(), Amount = 1 });

            return reservation;
        }
        #endregion

    }
}