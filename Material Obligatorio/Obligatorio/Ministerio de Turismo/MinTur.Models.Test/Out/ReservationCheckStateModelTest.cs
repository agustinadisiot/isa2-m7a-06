using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class ReservationCheckStateModelTest
    {
        [TestMethod]
        public void ReservationCheckStateModelCreatedAsExpected()
        {
            Reservation reservation = new Reservation()
            {
                Name = "Pedro",
                Surname = "Perez",
            };
            ReservationState newReservationState = new ReservationState()
            {
                Description = "We are waiting for your payment to arrive",
                State = "Waiting For Payment"
            };
            reservation.UpdateState(newReservationState);
            ReservationCheckStateModel reservationCheckStateModel = new ReservationCheckStateModel(reservation);

            Assert.IsTrue(reservation.ActualState.Description == reservationCheckStateModel.Description);
            Assert.IsTrue(reservation.Name == reservationCheckStateModel.Name);
            Assert.IsTrue(reservation.Surname == reservationCheckStateModel.Surname);
            Assert.IsTrue(reservationCheckStateModel.State.Equals("Waiting For Payment"));
        }
    }
}