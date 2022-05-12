using MinTur.Exceptions;

namespace MinTur.Models.In
{
    public class ResortPartialUpdateModel
    {
        public bool? NewAvailability { get; set; }

        public ResortPartialUpdateModel() { }

        public bool GetNewAvailabilty() 
        {
            if (!NewAvailability.HasValue)
                throw new InvalidRequestDataException("Must provide resort's new availability");

            return NewAvailability.HasValue && NewAvailability.Value;
        }
    }
}