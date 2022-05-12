using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.BusinessLogic.Pricing;
using System;

namespace MinTur.BusinessLogic.Test.Pricing
{
    [TestClass]
    public class ResortPricingCalculatorTest
    {
        private ResortPricingCalculator _resortPricingCalculator;
        private const int KidsDiscount = 2;
        private const int BabiesDiscount = 4;

        [TestInitialize]
        public void SetUp() 
        {
            _resortPricingCalculator = new ResortPricingCalculator();
        }

        [TestMethod]
        public void TotalPriceForAccommodationCalculatedCorrectly1() 
        {
            Resort resort = new Resort()
            {
                Id = 1,
                Name = "Hotel Italiano",
                PricePerNight = 126
            };
            Accommodation accommodation = new Accommodation()
            {
                CheckIn = new DateTime(2020, 9, 27),
                CheckOut = new DateTime(2020, 9, 30)
            };
            accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Adult.ToString(), Amount = 2 });
            accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Kid.ToString(), Amount = 4 });
            accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Baby.ToString(), Amount = 3 });
            accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Retired.ToString(), Amount = 3 });

            int expectedPrice = 2664;
            int totalPrice = _resortPricingCalculator.CalculateTotalPriceForAccommodation(resort, accommodation);
            Assert.AreEqual(expectedPrice, totalPrice);
        }

        [TestMethod]
        public void TotalPriceForAccommodationCalculatedCorrectly2()
        {
            Resort resort = new Resort()
            {
                Id = 1,
                Name = "Hotel Americano",
                PricePerNight = 200
            };
            Accommodation accommodation = new Accommodation()
            {
                CheckIn = new DateTime(2020, 9, 27),
                CheckOut = new DateTime(2020, 10, 7)
            };
            accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Adult.ToString(), Amount = 4 });
            accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Kid.ToString(), Amount = 2 });

            int expectedPrice = 10000;
            int totalPrice = _resortPricingCalculator.CalculateTotalPriceForAccommodation(resort, accommodation);
            Assert.AreEqual(expectedPrice, totalPrice);
        }

        [TestMethod]
        public void TotalPriceForAccommodationCalculatedCorrectly3()
        {
            Resort resort = new Resort()
            {
                Id = 1,
                Name = "Hotel Aleman",
                PricePerNight = 187
            };
            Accommodation accommodation = new Accommodation()
            {
                CheckIn = new DateTime(2020, 9, 27),
                CheckOut = new DateTime(2020, 9, 29)
            };
            accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Adult.ToString(), Amount = 4 });
            accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Baby.ToString(), Amount = 2 });

            int expectedPrice = 1683;
            int totalPrice = _resortPricingCalculator.CalculateTotalPriceForAccommodation(resort, accommodation);
            Assert.AreEqual(expectedPrice, totalPrice);
        }

        [TestMethod]
        public void TotalPriceForAccommodationCalculatedCorrectly4()
        {
            Resort resort = new Resort()
            {
                Id = 1,
                Name = "Hotel Ingles",
                PricePerNight = 364
            };
            Accommodation accommodation = new Accommodation()
            {
                CheckIn = new DateTime(2020, 9, 27),
                CheckOut = new DateTime(2020, 9, 28)
            };
            accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Adult.ToString(), Amount = 2 });
            accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Retired.ToString(), Amount = 2 });

            int expectedPrice = 1201;
            int totalPrice = _resortPricingCalculator.CalculateTotalPriceForAccommodation(resort, accommodation);
            Assert.AreEqual(expectedPrice, totalPrice);
        }

    }
}
