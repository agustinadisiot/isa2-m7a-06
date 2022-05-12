using MinTur.Domain.BusinessEntities;
using System.Linq;
using System.Collections.Generic;
using System;

namespace MinTur.Domain.Reports
{
    public class ReservationReport
    {
        public ReservationReportInput Input { get; set; }
        public List<KeyValuePair<Resort,int>> ReservationPerResort { get; private set; }
        
        public ReservationReport()
        {
            ReservationPerResort = new List<KeyValuePair<Resort, int>>();
        }

        public virtual void AddReportEntry(Resort resort)
        {
            if (!ResortBelongsToExpectedTouristPoint(resort))
                throw new InvalidOperationException();

            List<Reservation> resortReservations = new List<Reservation>(resort.Reservations);

            RemoveExpiredReservations(resortReservations);
            RemoveDeniedReservations(resortReservations);
            RemoveReservationThatDontMatchWithInput(resortReservations);

            ReservationPerResort.Add(new KeyValuePair<Resort, int>(resort, resortReservations.Count));
        }

        public virtual void RemoveEntriesWithNoReservations()
        {
            ReservationPerResort.RemoveAll(entry => entry.Value == 0);
        }

        public virtual void SortReportEntries()
        {
            ReservationPerResort.Sort(new ReservationReportEntryComparer());
            ReservationPerResort.Reverse();
        }

        public virtual bool ReportFailed()
        {
            return ReservationPerResort.Count == 0;
        }

        private bool ResortBelongsToExpectedTouristPoint(Resort resort)
        {
            return resort.TouristPointId == Input.TouristPointId;
        }

        private void RemoveDeniedReservations(List<Reservation> resortReservations)
        {
            resortReservations.RemoveAll(r => r.ActualState.State == PossibleReservationStates.Denied.ToString());
        }

        private void RemoveExpiredReservations(List<Reservation> resortReservations)
        {
            resortReservations.RemoveAll(r => r.ActualState.State == PossibleReservationStates.Expired.ToString());
        }


        private void RemoveReservationThatDontMatchWithInput(List<Reservation> resortReservations)
        {
            resortReservations.RemoveAll(r => !DatesIntersect(r.Accommodation));
        }

        private bool DatesIntersect(Accommodation accommodation)
        {
            bool datesIntersect = true;

            if (accommodation.CheckIn > Input.FinalDate || accommodation.CheckOut < Input.InitialDate)
                datesIntersect = false;

            return datesIntersect;
        }

    }
}
