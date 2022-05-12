using MinTur.Domain.BusinessEntities;
using MinTur.BusinessLogicInterface.Pricing;
using System;
using System.Collections.Generic;
using MinTur.Domain.DiscountPolicies.GuestGroups;

namespace MinTur.BusinessLogic.Pricing
{
    public class ResortPricingCalculator : IResortPricingCalculator
    {
        public int CalculateTotalPriceForAccommodation(Resort resort, Accommodation accommodation)
        {
            int totalPrice = 0;
            int pricePerNight = resort.PricePerNight;
            int amountOfNights = GetAmountOfNightsFromAccommodation(accommodation);
            List<GuestGroup> guestGroups = accommodation.Guests;

            foreach(GuestGroup guestGroup in guestGroups)
            {
                List<IGuestGroupDiscountPolicy> applicableDiscounts = guestGroup.GetApplicableDiscountPolicies();
                int guestsWithDiscount = 0;
                int guestsWithoutDiscount = guestGroup.Amount;

                if (applicableDiscounts.Count > 0)
                {
                    foreach (IGuestGroupDiscountPolicy policy in applicableDiscounts)
                    {
                        guestsWithDiscount = policy.AmountOfGuestsThatApplyForDiscount(guestGroup);
                        guestsWithoutDiscount -= guestsWithDiscount;
                        double discount = policy.GetAssociatedDiscount();

                        totalPrice += (int)(amountOfNights * guestsWithDiscount * pricePerNight * discount);
                        totalPrice += amountOfNights * guestsWithoutDiscount * pricePerNight;
                    }
                }
                else 
                    totalPrice += amountOfNights * guestsWithoutDiscount * pricePerNight;
            }

            return totalPrice;
        }

        private int GetAmountOfNightsFromAccommodation(Accommodation accommodation)
        {
            TimeSpan timespan = accommodation.CheckOut.Subtract(accommodation.CheckIn);
            return (int)Math.Ceiling(timespan.TotalDays);
        }


    }
}
