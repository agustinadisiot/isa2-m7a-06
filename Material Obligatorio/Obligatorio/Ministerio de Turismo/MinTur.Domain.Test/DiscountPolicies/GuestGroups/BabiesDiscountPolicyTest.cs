using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.DiscountPolicies.GuestGroups;

namespace MinTur.Domain.Test.DiscountPolicies.GuestGroups
{
    [TestClass]
    public class BabiesDiscountPolicyTest
    {
        private const double BabiesDiscount = 0.25;

        [TestMethod]
        public void PolicyAppliesToBabyGuests()
        {
            BabiesDiscountPolicy babiesDiscountPolicy = new BabiesDiscountPolicy();

            Assert.IsTrue(babiesDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Baby));
        }

        [TestMethod]
        public void PolicyDoesntApplyToAdultGuests()
        {
            BabiesDiscountPolicy babiesDiscountPolicy = new BabiesDiscountPolicy();

            Assert.IsFalse(babiesDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Adult));
        }

        [TestMethod]
        public void PolicyDoesntApplyToKidGuests()
        {
            BabiesDiscountPolicy babiesDiscountPolicy = new BabiesDiscountPolicy();

            Assert.IsFalse(babiesDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Kid));
        }

        [TestMethod]
        public void PolicyDoesntApplyToRetiredGuests()
        {
            BabiesDiscountPolicy babiesDiscountPolicy = new BabiesDiscountPolicy();

            Assert.IsFalse(babiesDiscountPolicy.PolicyAppliesToGuestGroup(GuestType.Retired));
        }

        [DataTestMethod]
        [DataRow(GuestType.Baby, 3)]
        [DataRow(GuestType.Baby, 1)]
        [DataRow(GuestType.Baby, 6)]
        public void DiscountAppliesToAllBabies(GuestType guestType, int amountGuests)
        {
            GuestGroup guestGroup = new GuestGroup()
            {
                GuestGroupType = guestType.ToString(),
                Amount = amountGuests
            };
            BabiesDiscountPolicy babiesDiscountPolicy = new BabiesDiscountPolicy();

            Assert.AreEqual(amountGuests, babiesDiscountPolicy.AmountOfGuestsThatApplyForDiscount(guestGroup));
        }

        [TestMethod]
        public void GetAssociatedMethodReturnsAsExpected()
        {
            BabiesDiscountPolicy babiesDiscountPolicy = new BabiesDiscountPolicy();

            Assert.AreEqual(BabiesDiscount, babiesDiscountPolicy.GetAssociatedDiscount());
        }

    }
}
