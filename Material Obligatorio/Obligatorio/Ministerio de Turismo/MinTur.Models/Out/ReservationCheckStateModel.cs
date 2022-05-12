using System;
using System.Text.RegularExpressions;
using MinTur.Domain.BusinessEntities;

namespace MinTur.Models.Out
{
    public class ReservationCheckStateModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string State { get; set; }

        public ReservationCheckStateModel(Reservation reservation)
        {
            Name = reservation.Name;
            Surname = reservation.Surname;
            Description = reservation.ActualState.Description;
            State = reservation.ActualState.State;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var reservationCheckStateModel = obj as ReservationCheckStateModel;
            return State == reservationCheckStateModel.State &&
                    Description == reservationCheckStateModel.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Description, State);
        }
    }
}