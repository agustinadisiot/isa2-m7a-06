using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MinTur.Domain.BusinessEntities
{
    public class Region
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<TouristPoint> TouristPoints { get; private set; }

        public Region()  
        {
            TouristPoints = new List<TouristPoint>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var region = obj as Region;
            return Id == region.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
