using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.DiscountPolicies.GuestGroups;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinTur.Domain.Test.DiscountPolicies.GuestGroups
{
    [TestClass]
    public class KidsDiscountPolicyTest
    {
        private const double KidsDiscount = 0.5;

        [TestMethod]
        public void PolicyAppliesToKidGuests()
        {
            KidsDiscountPolicy kidsDiscountPolicy = new KidsDiscountPolicy();

            Assert.IsTrue(kidsDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Kid));
        }

        [TestMethod]
        public void PolicyDoesntApplyToAdultGuests()
        {
            KidsDiscountPolicy kidsDiscountPolicy = new KidsDiscountPolicy();

            Assert.IsFalse(kidsDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Adult));
        }

        [TestMethod]
        public void PolicyDoesntApplyToBabyGuests()
        {
            KidsDiscountPolicy kidsDiscountPolicy = new KidsDiscountPolicy();

            Assert.IsFalse(kidsDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Baby));
        }

        [TestMethod]
        public void PolicyDoesntApplyToRetiredGuests()
        {
            KidsDiscountPolicy kidsDiscountPolicy = new KidsDiscountPolicy();

            Assert.IsFalse(kidsDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Retired));
        }

        [DataTestMethod]
        [DataRow(GuestType.Kid, 3)]
        [DataRow(GuestType.Kid, 1)]
        [DataRow(GuestType.Kid, 6)]
        public void DiscountAppliesToAllKids(GuestType guestType, int amountGuests)
        {
            GuestGroup guestGroup = new GuestGroup()
            {
                GuestGroupType = guestType.ToString(),
                Amount = amountGuests
            };
            KidsDiscountPolicy kidsDiscountPolicy = new KidsDiscountPolicy();

            Assert.AreEqual(amountGuests, kidsDiscountPolicy.AmountOfGuestsThatApplyForDiscount(guestGroup));
        }

        [TestMethod]
        public void GetAssociatedMethodReturnsAsExpected()
        {
            KidsDiscountPolicy kidsDiscountPolicy = new KidsDiscountPolicy();

            Assert.AreEqual(KidsDiscount, kidsDiscountPolicy.GetAssociatedDiscount());
        }
    }
}
