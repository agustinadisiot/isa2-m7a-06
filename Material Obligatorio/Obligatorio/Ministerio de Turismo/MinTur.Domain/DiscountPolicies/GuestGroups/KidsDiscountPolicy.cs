using MinTur.Domain.BusinessEntities;

namespace MinTur.Domain.DiscountPolicies.GuestGroups
{
    public class KidsDiscountPolicy : IGuestGroupDiscountPolicy
    {
        public const double Discount = 0.5;

        public bool PolicyAppliesToGuestGroup(GuestType guestGroupType)
        {
            bool policyApplies = false;

            if (guestGroupType == GuestType.Kid)
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
