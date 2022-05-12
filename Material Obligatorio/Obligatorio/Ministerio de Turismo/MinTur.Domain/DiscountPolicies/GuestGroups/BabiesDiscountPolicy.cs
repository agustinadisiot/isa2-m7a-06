using MinTur.Domain.BusinessEntities;

namespace MinTur.Domain.DiscountPolicies.GuestGroups
{
    public class BabiesDiscountPolicy : IGuestGroupDiscountPolicy
    {
        public const double Discount = 0.25;

        public bool PolicyAppliesToGuestGroup(GuestType guestGroupType)
        {
            bool policyApplies = false;

            if (guestGroupType == GuestType.Baby)
                policyApplies = true;

            return policyApplies;
        }

        public int AmountOfGuestsThatApplyForDiscount(GuestGroup guestGroup)
        {
            return guestGroup.Amount;
        }

        public double GetAssociatedDiscount()
        {
            return Discount;
        }

    }
}
