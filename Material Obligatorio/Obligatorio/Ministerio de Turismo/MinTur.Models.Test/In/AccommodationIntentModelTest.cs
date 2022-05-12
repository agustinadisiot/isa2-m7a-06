using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;
using System;

namespace MinTur.Models.Test.In
{
    [TestClass]
    public class AccommodationIntentModelTest
    {
        [TestMethod]
        public void ToEntityReturnsAsExpected() 
        {
            AccommodationIntentModel accommodationIntentModel = new AccommodationIntentModel()
            {
                CheckIn = new DateTime(2020, 9, 27),
                CheckOut = new DateTime(2020, 9, 29),
                AdultsAmount = 2,
                BabiesAmount = 1,
                KidsAmount = 2,
                RetiredAmount = 6
            };
            Accommodation accommodation = accommodationIntentModel.ToEntity();

            Assert.AreEqual(accommodation.CheckIn, accommodationIntentModel.CheckIn);
            Assert.AreEqual(accommodation.CheckOut, accommodationIntentModel.CheckOut);
            Assert.IsTrue(accommodation.Guests.Exists(g => g.GuestGroupType == GuestType.Adult.ToString() && g.Amount == accommodationIntentModel.AdultsAmount));
            Assert.IsTrue(accommodation.Guests.Exists(g => g.GuestGroupType == GuestType.Kid.ToString() && g.Amount == accommodationIntentModel.KidsAmount));
            Assert.IsTrue(accommodation.Guests.Exists(g => g.GuestGroupType == GuestType.Baby.ToString() && g.Amount == accommodationIntentModel.BabiesAmount));
            Assert.IsTrue(accommodation.Guests.Exists(g => g.GuestGroupType == GuestType.Retired.ToString() && g.Amount == accommodationIntentModel.RetiredAmount));
        }
    }
}
