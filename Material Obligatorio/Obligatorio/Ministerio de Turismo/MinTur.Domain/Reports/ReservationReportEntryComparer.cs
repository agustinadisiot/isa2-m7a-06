using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;

namespace MinTur.Domain.Reports
{
    internal class ReservationReportEntryComparer : IComparer<KeyValuePair<Resort, int>>
    {
        public int Compare(KeyValuePair<Resort, int> x, KeyValuePair<Resort, int> y)
        {
            int difference = x.Value - y.Value;

            if(difference == 0)
            {
                if (x.Key.MemberSince < y.Key.MemberSince)
                    difference = 1;
                else
                    difference = -1;
            }

            return difference;
        }
    }
}
