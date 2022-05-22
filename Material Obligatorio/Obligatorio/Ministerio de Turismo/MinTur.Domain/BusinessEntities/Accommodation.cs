using MinTur.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MinTur.Domain.BusinessEntities
{
    public class Accommodation
    {
        public int Id { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }
        [Required]
        public List<GuestGroup> Guests { get; set; }

        public Accommodation() 
        {
            Guests = new List<GuestGroup>();
        }

        public virtual bool ValidOrFail(DateTime currentTime) 
        {
            bool isValid = ValidateDates(currentTime);
            ValidateGuests();
            return isValid;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var acommodation = obj as Accommodation;
            return Id == acommodation.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        private bool ValidateDates(DateTime currentTime)
        {
            if (CheckIn == null || CheckOut == null || CheckIn < currentTime)
                throw new InvalidRequestDataException("Invalid Dates");
            else if (CheckOut <= CheckIn)
                throw new InvalidRequestDataException("Check-Out can not be on the same day or come before Check-In");
            else
                return true;
        }

        private void ValidateGuests()
        {
            if (!Guests.Exists(g => g.GuestGroupType == GuestType.Adult.ToString() && g.Amount > 0) 
                && !Guests.Exists(g => g.GuestGroupType == GuestType.Retired.ToString() && g.Amount > 0))
                throw new InvalidRequestDataException("There must be at least one adult or retired guest in order to book");

            Guests.ForEach(g => g.ValidOrFail());
        }

    }
}
