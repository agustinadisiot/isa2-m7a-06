using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MinTur.Domain.BusinessEntities
{
    public class TouristPointCategory
    {
        public int Id { get; set; }
        [Required]
        public int TouristPointId { get; set; }
        [Required]
        public TouristPoint TouristPoint { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var touristPointCategory = obj as TouristPointCategory;
            return CategoryId == touristPointCategory.CategoryId && TouristPointId == touristPointCategory.TouristPointId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TouristPointId, CategoryId);
        }
    }
}