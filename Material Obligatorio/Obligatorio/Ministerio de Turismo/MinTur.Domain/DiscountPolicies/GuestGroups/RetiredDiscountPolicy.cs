using MinTur.Domain.BusinessEntities;
using System;

namespace MinTur.Domain.DiscountPolicies.GuestGroups
{
    public class RetiredDiscountPolicy : IGuestGroupDiscountPolicy
    {
        public const double Discount = 0.3;

        public bool PolicyAppliesToGuestGroup(GuestType guestGroupType)
        {
            bool policyApplies = false;

            if (guestGroupType == GuestType.Retired)
                policyApplies = true;

            return policyApplies;
        }

        public int AmountOfGuestsThatApplyForDiscount(GuestGroup guestGroup)
        {
            return (int)Math.Floor(guestGroup.Amount / 2.0);
        }

        public double GetAssociatedDiscount()
        {
            return Discount;
        }

    }
}
