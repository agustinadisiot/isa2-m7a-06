using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Models.Out
{
    public class ReservationDetailsModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public int TotalPrice { get; set; }
        public int ResortId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public List<GuestBasicInfoModel> Guests { get; set; }

        public ReservationDetailsModel(Reservation reservation) 
        {
            Id = reservation.Id;
            Name = reservation.Name;
            Surname = reservation.Surname;
            Email = reservation.Email;
            TotalPrice = reservation.TotalPrice;
            ResortId = reservation.Resort.Id;
            CheckIn = reservation.Accommodation.CheckIn;
            CheckOut = reservation.Accommodation.CheckOut;
            State = reservation.ActualState.State;
            Description = reservation.ActualState.Description;
            Guests = reservation.Accommodation.Guests.Select(g => new GuestBasicInfoModel(g)).ToList();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var reservationDetailsModel = obj as ReservationDetailsModel;
            return State == reservationDetailsModel.State &&
                    Description == reservationDetailsModel.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
