using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MinTur.Exceptions;

namespace MinTur.Domain.BusinessEntities
{
    public class Resort
    {
        public int Id { get; set; }
        [Required]
        public int TouristPointId { get; set; }
        [Required]
        public TouristPoint TouristPoint { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Stars { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string ReservationMessage { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<Image> Images { get; set; }
        [Required]
        public int PricePerNight { get; set; }
        [Required]
        public bool Available { get; set; }
        [Required]
        public double Punctuation { get; set; }
        [Required]
        public DateTime MemberSince { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Reservation> Reservations { get; set; }

        public Resort()
        {
            Images = new List<Image>();
            Reviews = new List<Review>();
            Reservations = new List<Reservation>();
            Available = true;
            Punctuation = 0.0;
            MemberSince = DateTime.Now;
        }

        public virtual void ValidOrFail()
        {
            ValidateName();
            ValidateQuantityOfImages();
            ValidatePricePerNight();
            ValidateStars();
            ValidateAddress();
            ValidatePhoneNumber();
            ValidateDescription();
            ValidateReservationMessage();
        }

        public virtual void UpdateResortPunctuation(Review newReview)
        {
            double allReviewsSum = SumAllReviewsStars();
            double totalReviews = Reviews.Count;

            double newPunctuation = Math.Round((allReviewsSum + newReview.Stars) / (totalReviews + 1), 1);
            Punctuation = newPunctuation;
        }

        private int SumAllReviewsStars()
        {
            int total = 0;

            foreach(Review review in Reviews)
            {
                total += review.Stars;
            }

            return total;
        }

        private void ValidatePhoneNumber()
        {
            Regex nameRegex = new Regex(@"^[0-9]+$");

            if (PhoneNumber == null || !nameRegex.IsMatch(PhoneNumber))
                throw new InvalidRequestDataException("Invalid phone number - only integers with no spacing");
        }

        private void ValidateDescription()
        {
            if (Description == null)
                throw new InvalidRequestDataException("Must provide a description");
        }

        private void ValidateReservationMessage()
        {
            if (ReservationMessage == null)
                throw new InvalidRequestDataException("Must provide a reservation message");
        }

        private void ValidateAddress()
        {
            if(Address == null)
                throw new InvalidRequestDataException("Must provide an address");
        }

        private void ValidateName()
        {
            Regex nameRegex = new Regex(@"^[a-zA-ZñÑáéíóúü ]+$");

            if (Name == null || !nameRegex.IsMatch(Name))
                throw new InvalidRequestDataException("Invalid name");
        }

        private void ValidateQuantityOfImages()
        {
            if (Images.Count < 1)
            {
                throw new InvalidRequestDataException("Must include at least one image");
            }
        }

        private void ValidatePricePerNight()
        {
            if (PricePerNight <= 0)
            {
                throw new InvalidRequestDataException("Invalid price per night");
            }
        }

        private void ValidateStars()
        {
            if (Stars < 1 || Stars > 5)
            {
                throw new InvalidRequestDataException("Invalid stars value - must be between 1 and 5");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var resort = obj as Resort;
            return Id == resort.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

    }
}
