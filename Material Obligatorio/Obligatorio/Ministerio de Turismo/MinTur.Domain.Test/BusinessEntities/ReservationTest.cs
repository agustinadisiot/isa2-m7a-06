using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace MinTur.Domain.Test.BusinessEntities
{
    [TestClass]
    public class ReservationTest
    {
        [TestMethod]
        public void ValidReservationPassesValidation()
        {
            Mock<Accommodation> accomodationMock = new Mock<Accommodation>();
            accomodationMock.Setup(a => a.ValidOrFail(It.IsAny<DateTime>()));

            Reservation reservation = new Reservation()
            {
                Name = "Pedro",
                Surname = "Perez",
                Email = "pedroPerez@gmail.com",
                Resort = new Resort()
                {
                    Id = 9,
                    Name = "Hotel Italiano"
                },
                Accommodation = accomodationMock.Object
            };

            reservation.ValidOrFail(DateTime.MinValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ReservationWithInvalidNameFails()
        {
            Mock<Accommodation> accomodationMock = new Mock<Accommodation>();
            accomodationMock.Setup(a => a.ValidOrFail(It.IsAny<DateTime>()));

            Reservation reservation = new Reservation()
            {
                Name = "988gedwqas-!",
                Surname = "Perez",
                Email = "pedroPerez@gmail.com",
                Resort = new Resort()
                {
                    Id = 9,
                    Name = "Hotel Italiano"
                },
                Accommodation = accomodationMock.Object
            };

            reservation.ValidOrFail(DateTime.MinValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ReservationWithInvalidSurnameFails()
        {
            Mock<Accommodation> accomodationMock = new Mock<Accommodation>();
            accomodationMock.Setup(a => a.ValidOrFail(It.IsAny<DateTime>()));

            Reservation reservation = new Reservation()
            {
                Name = "Pedro",
                Surname = "8weew**/!",
                Email = "pedroPerez@gmail.com",
                Resort = new Resort()
                {
                    Id = 9,
                    Name = "Hotel Italiano"
                },
                Accommodation = accomodationMock.Object
            };

            reservation.ValidOrFail(DateTime.MinValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ReservationWithInvalidEmailFails()
        {
            Mock<Accommodation> accomodationMock = new Mock<Accommodation>();
            accomodationMock.Setup(a => a.ValidOrFail(It.IsAny<DateTime>()));

            Reservation reservation = new Reservation()
            {
                Name = "Pedro",
                Surname = "8weew**/!",
                Email = "pedroPerezJejejeMailMalogmail/com..",
                Resort = new Resort()
                {
                    Id = 9,
                    Name = "Hotel Italiano"
                },
                Accommodation = accomodationMock.Object
            };

            reservation.ValidOrFail(DateTime.MinValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ReservationWithInvalidAccommodationFails()
        {
            Mock<Accommodation> accomodationMock = new Mock<Accommodation>();
            accomodationMock.Setup(a => a.ValidOrFail(It.IsAny<DateTime>())).Throws(new InvalidRequestDataException(""));

            Reservation reservation = new Reservation()
            {
                Name = "Pedro",
                Surname = "8weew**/!",
                Email = "pedroPerez@gmail.com",
                Resort = new Resort()
                {
                    Id = 9,
                    Name = "Hotel Italiano"
                },
                Accommodation = accomodationMock.Object
            };

            reservation.ValidOrFail(DateTime.MinValue);
        }

        [TestMethod]
        public void UpdateStateWithValidNewStateDoesAsExpected()
        {
            Reservation reservation = new Reservation()
            {
                Name = "Pedro",
                Surname = "Perez",
                Email = "pedroPerez@gmail.com"
            };
            ReservationState newState = new ReservationState()
            {
                Id = 2,
                Description = "Description...",
                State = "Accepted"
            };

            reservation.UpdateState(newState);
            Assert.AreEqual(newState.State, reservation.ActualState.State);
            Assert.AreEqual(newState.Description, reservation.ActualState.Description);
        }
    }
}
