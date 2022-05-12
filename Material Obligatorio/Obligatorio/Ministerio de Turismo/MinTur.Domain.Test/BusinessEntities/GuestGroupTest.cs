using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.DiscountPolicies.GuestGroups;
using MinTur.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Domain.Test.BusinessEntities
{
    [TestClass]
    public class GuestGroupTest
    {
        [TestMethod]
        public void ValidGuestGroupPassesValidation()
        {
            GuestGroup guestGroup = new GuestGroup()
            {
                GuestGroupType = GuestType.Kid.ToString(),
                Amount = 4
            };

            guestGroup.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void GuestGroupWithNegativeAmountFailsValidation()
        {
            GuestGroup guestGroup = new GuestGroup()
            {
                GuestGroupType = GuestType.Adult.ToString(),
                Amount = -2
            };

            guestGroup.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void GuestGroupWithInvalidGuestTypeFailsValidation()
        {
            GuestGroup guestGroup = new GuestGroup()
            {
                GuestGroupType = "Perros",
                Amount = 3
            };

            guestGroup.ValidOrFail();
        }

        [DataTestMethod]
        [DataRow(GuestType.Kid, typeof(KidsDiscountPolicy))]
        [DataRow(GuestType.Baby, typeof(BabiesDiscountPolicy))]
        [DataRow(GuestType.Retired, typeof(RetiredDiscountPolicy))]
        public void GetApplicableDiscountPoliciesReturnsAsExpected(GuestType guestType, Type discountType)
        {
            GuestGroup guestGroup = new GuestGroup() 
            { 
                GuestGroupType = guestType.ToString() 
            };
            List<IGuestGroupDiscountPolicy> discountPoliciesRetrieved = guestGroup.GetApplicableDiscountPolicies();

            Assert.AreEqual(1, discountPoliciesRetrieved.Count);
            Assert.IsTrue(discountPoliciesRetrieved.All(dp => dp.GetType() == discountType));
        }
    }
}
