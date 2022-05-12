using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.DiscountPolicies.GuestGroups;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinTur.Domain.Test.DiscountPolicies.GuestGroups
{
    [TestClass]
    public class RetiredDiscountPolicyTest
    {
        private const double RetiredDiscount = 0.3;

        [TestMethod]
        public void PolicyAppliesToRetiredGuests()
        {
            RetiredDiscountPolicy retiredDiscountPolicy = new RetiredDiscountPolicy();

            Assert.IsTrue(retiredDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Retired));
        }

        [TestMethod]
        public void PolicyDoesntApplyToAdultGuests()
        {
            RetiredDiscountPolicy retiredDiscountPolicy = new RetiredDiscountPolicy();

            Assert.IsFalse(retiredDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Adult));
        }

        [TestMethod]
        public void PolicyDoesntApplyToBabyGuests()
        {
            RetiredDiscountPolicy retiredDiscountPolicy = new RetiredDiscountPolicy();

            Assert.IsFalse(retiredDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Baby));
        }

        [TestMethod]
        public void PolicyDoesntApplyToKidGuests()
        {
            RetiredDiscountPolicy retiredDiscountPolicy = new RetiredDiscountPolicy();

            Assert.IsFalse(retiredDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Kid));
        }

        [DataTestMethod]
        [DataRow(GuestType.Retired, 3)]
        [DataRow(GuestType.Retired, 1)]
        [DataRow(GuestType.Retired, 6)]
        public void DiscountAppliesToEveryTwoRetiredGuests(GuestType guestType, int amountGuests)
        {
            GuestGroup guestGroup = new GuestGroup()
            {
                GuestGroupType = guestType.ToString(),
                Amount = amountGuests
            };
            RetiredDiscountPolicy retiredDiscountPolicy = new RetiredDiscountPolicy();

            Assert.AreEqual((int)Math.Floor(amountGuests / 2.0), retiredDiscountPolicy.AmountOfGuestsThatApplyForDiscount(guestGroup));
        }

        [TestMethod]
        public void GetAssociatedMethodReturnsAsExpected()
        {
            RetiredDiscountPolicy retiredDiscountPolicy = new RetiredDiscountPolicy();

            Assert.AreEqual(RetiredDiscount, retiredDiscountPolicy.GetAssociatedDiscount());
        }
    }
}
