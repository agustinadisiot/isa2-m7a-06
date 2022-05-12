using MinTur.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MinTur.Domain.BusinessEntities
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int Stars { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int ResortId { get; set; }
        [Required]
        public Guid ReservationId { get; set; }

        public virtual void ValidOrFail()
        {
            ValidateName();
            ValidateSurname();
            ValidateStars();
            ValidateText();
        }

        private void ValidateText()
        {
            if (Text == null)
                throw new InvalidRequestDataException("Must provide a valid text to the review");
        }

        private void ValidateStars()
        {
            if (Stars < 1 || Stars > 5)
                throw new InvalidRequestDataException("Review stars must be an integer between 1 and 5");
        }

        private void ValidateSurname()
        {
            Regex nameRegex = new Regex(@"^[a-zA-ZñÑáéíóúü ]+$");

            if (Surname == null || !nameRegex.IsMatch(Surname))
                throw new InvalidRequestDataException("Invalid name");
        }

        private void ValidateName()
        {
            Regex nameRegex = new Regex(@"^[a-zA-ZñÑáéíóúü ]+$");

            if (Name == null || !nameRegex.IsMatch(Name))
                throw new InvalidRequestDataException("Invalid name");
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var review = obj as Review;
            return Id == review.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

    }
}
