using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;

namespace MinTur.Models.Test.In
{
    [TestClass]
    public class ReservationStateIntentModelTest
    {
        [TestMethod]
        public void ToEntityReturnsAsExpected() 
        {
            ReservationStateIntentModel reservationStateIntentModel = new ReservationStateIntentModel()
            {
                State = "Waiting For Payment",
                Description = "We are waiting for your bank's confirmation"
            };
            ReservationState reservationState = reservationStateIntentModel.ToEntity();

            Assert.AreEqual(reservationStateIntentModel.Description, reservationState.Description);
            Assert.AreEqual(reservationStateIntentModel.State, reservationState.State);
        }
    }
}
