using MinTur.Domain.BusinessEntities;
using System;

namespace MinTur.Models.Out
{
    public class GuestBasicInfoModel
    {
        public string GuestType { get; set; }
        public int Ammount { get; set; }

        public GuestBasicInfoModel(GuestGroup guestGroup)
        {
            GuestType = guestGroup.GuestGroupType;
            Ammount = guestGroup.Amount;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var guestBasicInfoModel = obj as GuestBasicInfoModel;
            return GuestType == guestBasicInfoModel.GuestType && Ammount == guestBasicInfoModel.Ammount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GuestType, Ammount);
        }
    }
}
