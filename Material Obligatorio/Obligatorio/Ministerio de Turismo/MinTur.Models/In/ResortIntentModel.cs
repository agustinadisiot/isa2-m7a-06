using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;
using System.Linq;
using MinTur.Exceptions;

namespace MinTur.Models.In
{
    public class ResortIntentModel
    {
        public int TouristPointId { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ReservationMessage { get; set; }
        public string Description { get; set; }
        public List<string> ImagesData { get; set; }
        public int PricePerNight { get; set; }
        public bool? Available { get; set; }

        public Resort ToEntity()
        {
            return new Resort()
            {
                TouristPointId = TouristPointId,
                Name = Name,
                Stars = Stars,
                Address = Address,
                PhoneNumber = PhoneNumber,
                ReservationMessage = ReservationMessage,
                Description = Description,
                Images = ImagesData != null ? ImagesData.Select(imageData => new Image()
                {
                    Data = imageData
                }).ToList() : new List<Image>(),
                PricePerNight = PricePerNight,
                Available = GetResortAvailability()
            };
        }

        private bool GetResortAvailability() 
        {
            if (!Available.HasValue)
                throw new InvalidRequestDataException("Must provide resort's availability");

            return Available.HasValue && Available.Value;
        }
    }
}