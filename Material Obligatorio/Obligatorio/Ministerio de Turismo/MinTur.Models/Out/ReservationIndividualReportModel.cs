using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;

namespace MinTur.Models.Out
{
    public class ReservationIndividualReportModel
    {
        public string ResortName { get; set; }
        public int AmountOfReservations { get; set; }

        public ReservationIndividualReportModel(KeyValuePair<Resort, int> individualReport)
        {
            ResortName = individualReport.Key.Name;
            AmountOfReservations = individualReport.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var model = obj as ReservationIndividualReportModel;
            return ResortName == model.ResortName && AmountOfReservations == model.AmountOfReservations;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ResortName, AmountOfReservations);
        }
    }
}
