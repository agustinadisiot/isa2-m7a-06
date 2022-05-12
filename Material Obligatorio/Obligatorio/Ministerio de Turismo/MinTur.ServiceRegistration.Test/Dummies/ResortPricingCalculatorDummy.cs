using MinTur.Domain.BusinessEntities;
using MinTur.BusinessLogicInterface.Pricing;
using System;

namespace MinTur.ServiceRegistration.Test.Dummies
{
    public class ResortPricingCalculatorDummy : IResortPricingCalculator
    {
        public int CalculateTotalPriceForAccommodation(Resort resort, Accommodation accommodation)
        {
            throw new NotImplementedException();
        }
    }
}
