using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MinTur.Domain.Test.BusinessEntities
{
    [TestClass]
    public class ReservationStateTest
    {
        [TestMethod]
        public void InitialReservationStateIsValid() 
        {
            ReservationState reservationState = new ReservationState();
            reservationState.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ReservationStateWithNoDescription()
        {
            ReservationState reservationState = new ReservationState()
            {
                State = "Not a valid State",
                Description = ""
            };
            reservationState.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ReservationStateWithInvalidPossibleState() 
        {
            ReservationState reservationState = new ReservationState()
            { 
                State = "Not a valid State"
            };
            reservationState.ValidOrFail();
        }

        [TestMethod]
        public void ReservationStateWithValidWaitingForPaymentState()
        {
            ReservationState reservationState = new ReservationState()
            {
                State = "Waiting For Payment"
            };
            reservationState.ValidOrFail();
        }

        [TestMethod]
        public void ReservationStateWithValidAcceptedState()
        {
            ReservationState reservationState = new ReservationState()
            {
                State = "Accepted"
            };
            reservationState.ValidOrFail();
        }

        [TestMethod]
        public void ReservationStateWithValidDeniedState()
        {
            ReservationState reservationState = new ReservationState()
            {
                State = "Denied"
            };
            reservationState.ValidOrFail();
        }

        [TestMethod]
        public void ReservationStateWithValidExpiredState()
        {
            ReservationState reservationState = new ReservationState()
            {
                State = "Expired"
            };
            reservationState.ValidOrFail();
        }

    }
}
