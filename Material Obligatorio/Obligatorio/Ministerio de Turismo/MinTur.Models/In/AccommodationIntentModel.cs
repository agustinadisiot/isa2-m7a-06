using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinTur.Models.In
{
    public class AccommodationIntentModel
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int AdultsAmount { get; set; }
        public int KidsAmount { get; set; }
        public int BabiesAmount { get; set; }
        public int RetiredAmount { get; set; }

        public AccommodationIntentModel() { }

        public Accommodation ToEntity()
        {
            Accommodation accommodation = new Accommodation() 
            {
                CheckIn = CheckIn,
                CheckOut = CheckOut
            };
            if(AdultsAmount > 0)
                accommodation.Guests.Add(new GuestGroup() { Amount = AdultsAmount, GuestGroupType = GuestType.Adult.ToString() });
            if (KidsAmount > 0)
                accommodation.Guests.Add(new GuestGroup() { Amount = KidsAmount, GuestGroupType = GuestType.Kid.ToString() });
            if (BabiesAmount > 0)
                accommodation.Guests.Add(new GuestGroup() { Amount = BabiesAmount, GuestGroupType = GuestType.Baby.ToString() });
            if (RetiredAmount > 0)
                accommodation.Guests.Add(new GuestGroup() { Amount = RetiredAmount, GuestGroupType = GuestType.Retired.ToString() });

            return accommodation;
        }

    }
}
