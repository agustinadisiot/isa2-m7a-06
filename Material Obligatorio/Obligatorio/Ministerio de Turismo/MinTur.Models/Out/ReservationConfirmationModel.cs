using MinTur.Domain.BusinessEntities;
using System;

namespace MinTur.Models.Out
{
    public class ReservationConfirmationModel
    {
        public string UniqueCode { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ResortReservationMessage { get; set; }

        public ReservationConfirmationModel(Reservation reservation) 
        {
            UniqueCode = reservation.Id.ToString();
            ContactPhoneNumber = reservation.Resort.PhoneNumber;
            ResortReservationMessage = reservation.Resort.ReservationMessage;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var reservationConfirmationModel = obj as ReservationConfirmationModel;
            return UniqueCode == reservationConfirmationModel.UniqueCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UniqueCode);
        }

    }
}
