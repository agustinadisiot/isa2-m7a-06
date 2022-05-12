using MinTur.Domain.BusinessEntities;

namespace MinTur.BusinessLogicInterface.Pricing
{
    public interface IResortPricingCalculator
    {
        int CalculateTotalPriceForAccommodation(Resort resort, Accommodation accommodation);
    }
}
