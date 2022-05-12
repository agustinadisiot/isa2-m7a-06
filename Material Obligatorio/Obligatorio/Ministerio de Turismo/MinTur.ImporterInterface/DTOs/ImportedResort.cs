using System;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.ImporterInterface.DTOs
{
    public class ImportedResort
    {
        public ImportedTouristPoint TouristPoint { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ReservationMessage { get; set; }
        public string Description { get; set; }
        public List<string> ImagesData { get; set; }
        public int PricePerNight { get; set; }
        public bool Available { get; set; }

        public ImportedResort()
        {
            Available = true;
            ImagesData = new List<string>();
            TouristPoint = new ImportedTouristPoint();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var resort = obj as ImportedResort;
            return Name == resort.Name && Stars == resort.Stars && Address == resort.Address
                && PhoneNumber == resort.PhoneNumber && ReservationMessage == resort.ReservationMessage
                && Description == resort.Description && ImagesData.SequenceEqual(resort.ImagesData)
                && PricePerNight == resort.PricePerNight && Available == resort.Available
                && TouristPoint.Equals(resort.TouristPoint);
        }

        
    }
}
