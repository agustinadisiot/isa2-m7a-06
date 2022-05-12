using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinTur.Models.In
{
    public class ReservationIntentModel
    {
        public int ResortId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int AdultsAmount { get; set; }
        public int KidsAmount { get; set; }
        public int BabiesAmount { get; set; }
        public int RetiredAmount { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public Reservation ToEntity() 
        {
            Reservation reservation =  new Reservation()
            {
                Accommodation = new Accommodation()
                {
                    CheckIn = CheckIn,
                    CheckOut = CheckOut
                },
                Name = Name,
                Surname = Surname,
                Email = Email,
                Resort = new Resort() { Id = ResortId }
            };
            
            if (AdultsAmount > 0)
                reservation.Accommodation.Guests.Add(new GuestGroup() { Amount = AdultsAmount, GuestGroupType = GuestType.Adult.ToString() });
            if (KidsAmount > 0)
                reservation.Accommodation.Guests.Add(new GuestGroup() { Amount = KidsAmount, GuestGroupType = GuestType.Kid.ToString() });
            if (BabiesAmount > 0)
                reservation.Accommodation.Guests.Add(new GuestGroup() { Amount = BabiesAmount, GuestGroupType = GuestType.Baby.ToString() });
            if (RetiredAmount > 0)
                reservation.Accommodation.Guests.Add(new GuestGroup() { Amount = RetiredAmount, GuestGroupType = GuestType.Retired.ToString() });

            return reservation;
        }
    }
}
