using MinTur.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace MinTur.Domain.BusinessEntities
{
    public class Reservation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public virtual Resort Resort { get; set; }
        [Required]
        public virtual Accommodation Accommodation { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public ReservationState ActualState { get; private set; }
        [Required]
        public int TotalPrice { get; set; }

        public Reservation()
        {
            ActualState = new ReservationState();
        }

        public virtual void ValidOrFail(DateTime currentTime)
        {
            ValidateName();
            ValidateSurname();
            ValidateEmail();
            ValidateAccommodation(currentTime);
        }

        public virtual void UpdateState(ReservationState reservationState)
        {
            ActualState.Description = reservationState.Description;
            ActualState.State = reservationState.State;
        }

        private void ValidateName()
        {
            Regex nameRegex = new Regex(@"^[a-zA-ZÒ—·ÈÌÛ˙¸ ]+$");

            if (Name == null || !nameRegex.IsMatch(Name))
                throw new InvalidRequestDataException("Invalid name");
        }
        private void ValidateSurname()
        {
            Regex surnameRegex = new Regex(@"^[a-zA-ZÒ—·ÈÌÛ˙¸ ]+$");

            if (Surname == null || !surnameRegex.IsMatch(Surname))
                throw new InvalidRequestDataException("Invalid surname");
        }
        private void ValidateEmail()
        {
            Regex mailRegex = new Regex(@"^[a-zA-Z0-9_]+\@[a-zA-Z0-9_]+\.[a-zA-Z]+\.*[a-zA-Z]*\.*[a-zA-Z]*$");

            if (Email == null || !mailRegex.IsMatch(Email))
                throw new InvalidRequestDataException("Invalid email");
        }
        private void ValidateAccommodation(DateTime currentTime)
        {
            Accommodation.ValidOrFail(currentTime);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var reservation = obj as Reservation;
            return Id == reservation.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
      
    }
}
