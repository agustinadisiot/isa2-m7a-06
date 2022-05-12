using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;
using System;
using System.Linq;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class ReservationDetailsModelTest
    {
        [TestMethod]
        public void ReservationDetailsModelGetsCreatedAsExpected() 
        {
            Reservation reservation = new Reservation()
            {
                Id = Guid.NewGuid(),
                Name = "Pedro",
                Surname = "Perez",
                Email = "pedro@perez.com",
                Resort = new Resort() { Id = 5 },
                TotalPrice = 500,
                Accommodation = new Accommodation()
                {
                    CheckIn = new DateTime(2020, 10, 10),
                    CheckOut = new DateTime(2020, 10, 20)
                }
            };
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Adult.ToString(), Amount = 3 });
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Baby.ToString(), Amount = 1 });
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Kid.ToString(), Amount = 4 });
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Retired.ToString(), Amount = 2 });

            ReservationDetailsModel reservationDetailsModel = new ReservationDetailsModel(reservation);

            Assert.AreEqual(reservation.Id, reservationDetailsModel.Id);
            Assert.AreEqual(reservation.Name, reservationDetailsModel.Name);
            Assert.AreEqual(reservation.Surname, reservationDetailsModel.Surname);
            Assert.AreEqual(reservation.Email, reservationDetailsModel.Email);
            Assert.AreEqual(reservation.Resort.Id, reservationDetailsModel.ResortId);
            Assert.AreEqual(reservation.TotalPrice, reservationDetailsModel.TotalPrice);
            Assert.AreEqual(reservation.Accommodation.CheckIn, reservationDetailsModel.CheckIn);
            Assert.AreEqual(reservation.Accommodation.CheckOut, reservationDetailsModel.CheckOut);
            Assert.AreEqual(reservation.ActualState.Description, reservationDetailsModel.Description);
            Assert.AreEqual(reservation.ActualState.State, reservationDetailsModel.State);
            CollectionAssert.AreEquivalent(reservationDetailsModel.Guests, reservation.Accommodation.Guests.Select(g => new GuestBasicInfoModel(g)).ToList());
        }
    }
}
