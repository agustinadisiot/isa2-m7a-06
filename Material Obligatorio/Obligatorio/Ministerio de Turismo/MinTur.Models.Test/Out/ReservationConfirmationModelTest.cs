using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;
using System;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class ReservationConfirmationModelTest
    {
        [TestMethod]
        public void ReservationConfirmationModelGetsCreatedAsExpected() 
        {
            Reservation reservation = new Reservation()
            {
                Resort = new Resort()
                {
                    PhoneNumber = "095784685",
                    ReservationMessage = "Thanks for choosing us ..."
                }
            };
            ReservationConfirmationModel reservationConfirmationModel = new ReservationConfirmationModel(reservation);

            Assert.IsTrue(reservation.Id == new Guid(reservationConfirmationModel.UniqueCode));
            Assert.IsTrue(reservation.Resort.PhoneNumber == reservationConfirmationModel.ContactPhoneNumber);
            Assert.IsTrue(reservation.Resort.ReservationMessage == reservationConfirmationModel.ResortReservationMessage);
        }
    }
}
