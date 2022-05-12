using MinTur.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MinTur.Domain.BusinessEntities
{
    public class TouristPoint
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }
        [Required]
        public Image Image { get; set; }
        [Required]
        public int RegionId { get; set; }
        [Required]
        public Region Region { get; set; }
        public List<TouristPointCategory> TouristPointCategory { get; private set; }
        public List<Resort> Resorts { get; private set; }

        public TouristPoint()
        {
            TouristPointCategory = new List<TouristPointCategory>();
            Resorts = new List<Resort>();
        }

        public virtual void ValidOrFail() 
        {
            ValidateName();
            ValidateDescription();
            ValidateImage();
            ValidateCategories();
        }

        private void ValidateName()
        {
            Regex nameRegex = new Regex(@"^[a-zA-ZñÑáéíóúü0-9 ]+$");

            if (Name == null || !nameRegex.IsMatch(Name))
                throw new InvalidRequestDataException("Invalid tourist point name - only alphanumeric");
        }

        private void ValidateDescription() 
        {
            if (Description == null || Description.Length > 2000)
                throw new InvalidRequestDataException("Invalid description - only up to 2000 characters");
        }

        private void ValidateImage() 
        {
            if (Image == null)
                throw new InvalidRequestDataException("Must provide an image for the tourist point");

            Image.ValidOrFail();
        }

        private void ValidateCategories()
        {
            if (TouristPointCategory.Count < 1)
                throw new InvalidRequestDataException("Must provide at least one category for this tourist point");
        }

        public void AddCategory(Category category)
        {
            TouristPointCategory touristPointCategory = new TouristPointCategory()
            {
                TouristPointId = Id,
                TouristPoint = this,
                CategoryId = category.Id,
                Category = category
            };

            TouristPointCategory.Add(touristPointCategory);
        }

        public void RemoveCategory(Category category)
        {
            TouristPointCategory touristPointCategory = new TouristPointCategory()
            {
                TouristPointId = Id,
                TouristPoint = this,
                CategoryId = category.Id,
                Category = category
            };

            TouristPointCategory.Remove(touristPointCategory);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var touristPoint = obj as TouristPoint;
            return Id == touristPoint.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

    }
}
