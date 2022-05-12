using MinTur.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MinTur.Domain.BusinessEntities
{
    public enum PossibleReservationStates { Created, WaitingForPayment, Accepted, Denied, Expired, Invalid }

    public class ReservationState
    {
        private const PossibleReservationStates InitialState = PossibleReservationStates.Created;
        private const string InitialDescription = "We received your reservation correctly, we will let you know as soon as we finish processing it!";
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        private PossibleReservationStates _state;
        [Required]
        public string State 
        {
            get 
            { 
                return Regex.Replace(_state.ToString(), "([a-z])([A-Z])", "$1 $2"); 
            }
            set 
            {
                if (value == null || !Enum.TryParse(value.Replace(" ", string.Empty), out PossibleReservationStates newState))
                    _state = PossibleReservationStates.Invalid;
                else
                    _state = newState;
            }
        }

        public ReservationState() 
        {
            Description = InitialDescription;
            _state = InitialState;
        }

        public virtual void ValidOrFail() 
        {
            ValidateState();
            ValidateDescription();
        }

        private void ValidateDescription()
        {
            if(Description == null || Description == string.Empty)
                throw new InvalidRequestDataException("Must provide a valid Description");
        }

        private void ValidateState() 
        {
            if (_state == PossibleReservationStates.Invalid)
                throw new InvalidRequestDataException("The only valid reservation states are: 'Created', 'Waiting For Payment', 'Accepted', 'Denied' and 'Expired'");
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var reservationState = obj as ReservationState;
            return Id == reservationState.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

    }
}
