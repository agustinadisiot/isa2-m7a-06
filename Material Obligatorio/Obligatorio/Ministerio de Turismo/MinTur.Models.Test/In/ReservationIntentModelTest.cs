using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;
using System;

namespace MinTur.Models.Test.In
{
    [TestClass]
    public class ReservationIntentModelTest
    {
        [TestMethod]
        public void ToEntityReturnsAsExpected() 
        {
            ReservationIntentModel reservationIntentModel = new ReservationIntentModel()
            {
                ResortId = 5,
                Name = "Pedro",
                Surname = "Perez",
                Email = "pedroPerez@gmail.com",
                CheckIn = new DateTime(2020,9,28),
                CheckOut = new DateTime(2020,10,7),
                AdultsAmount = 2,
                KidsAmount = 1,
                BabiesAmount = 1,
                RetiredAmount = 1
            };
            Reservation reservation = reservationIntentModel.ToEntity();

            Assert.AreEqual(reservationIntentModel.ResortId, reservation.Resort.Id);
            Assert.AreEqual(reservationIntentModel.Name, reservation.Name);
            Assert.AreEqual(reservationIntentModel.Surname, reservation.Surname);
            Assert.AreEqual(reservationIntentModel.Email, reservation.Email);
            Assert.AreEqual(reservationIntentModel.CheckIn, reservation.Accommodation.CheckIn);
            Assert.AreEqual(reservationIntentModel.CheckOut, reservation.Accommodation.CheckOut);
            Assert.IsTrue(reservation.Accommodation.Guests.Exists(g => g.GuestGroupType == GuestType.Adult.ToString() && g.Amount == reservationIntentModel.AdultsAmount));
            Assert.IsTrue(reservation.Accommodation.Guests.Exists(g => g.GuestGroupType == GuestType.Kid.ToString() && g.Amount == reservationIntentModel.KidsAmount));
            Assert.IsTrue(reservation.Accommodation.Guests.Exists(g => g.GuestGroupType == GuestType.Baby.ToString() && g.Amount == reservationIntentModel.BabiesAmount));
            Assert.IsTrue(reservation.Accommodation.Guests.Exists(g => g.GuestGroupType == GuestType.Retired.ToString() && g.Amount == reservationIntentModel.RetiredAmount));
        }
    }
}
