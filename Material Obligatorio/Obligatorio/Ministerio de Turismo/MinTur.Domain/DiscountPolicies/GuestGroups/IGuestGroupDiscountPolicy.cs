using MinTur.Domain.BusinessEntities;

namespace MinTur.Domain.DiscountPolicies.GuestGroups
{
    public interface IGuestGroupDiscountPolicy
    {
        bool PolicyAppliesToGuestGroup(GuestType guestGroupType);
        int AmountOfGuestsThatApplyForDiscount(GuestGroup guestGroup);
        double GetAssociatedDiscount();
    }
}
