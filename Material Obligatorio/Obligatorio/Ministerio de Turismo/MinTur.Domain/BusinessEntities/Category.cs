using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MinTur.Domain.BusinessEntities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<TouristPointCategory> TouristPointCategory { get; private set; }

        public Category() 
        {
            TouristPointCategory = new List<TouristPointCategory>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var category = obj as Category;
            return Id == category.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
