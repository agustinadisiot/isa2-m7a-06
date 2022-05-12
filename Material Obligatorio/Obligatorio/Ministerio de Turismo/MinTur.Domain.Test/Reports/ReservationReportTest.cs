using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.Reports;
using System;
using System.Collections.Generic;

namespace MinTur.Domain.Test.Reports
{
    [TestClass]
    public class ReservationReportTest
    {
        private List<KeyValuePair<Resort, int>> _reservationsPerResort;
        private List<KeyValuePair<Resort, int>> _sortedReservationsPerResort;

        #region
        [TestInitialize]
        public void SetUp()
        {
            _reservationsPerResort = new List<KeyValuePair<Resort, int>>();
            _sortedReservationsPerResort = new List<KeyValuePair<Resort, int>>();
        }
        #endregion

        [TestMethod]
        public void AddReportEntryDoesAsExpected1()
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Name = "Hotel Italiano",
                TouristPointId = 2,
                Reservations = new List<Reservation>()
                {
                    CreateAcceptedReservation(new DateTime(2020,10,10), new DateTime(2020,11,13)),
                    CreateCreatedReservation(new DateTime(2020,10,6), new DateTime(2020,10,14)),
                    CreateWaitingForPaymentReservation(new DateTime(2020,10,10), new DateTime(2020,10,13)),
                    CreateWaitingForPaymentReservation(new DateTime(2020,7,20), new DateTime(2020,8,10))
                }
            };
            ReservationReport report = new ReservationReport() 
            {
                Input = new ReservationReportInput()
                {
                    InitialDate = new DateTime(2020,10,6),
                    FinalDate = new DateTime(2020,10,13),
                    TouristPointId = 2
                }
            };

            report.AddReportEntry(resort);
            CollectionAssert.Contains(report.ReservationPerResort, new KeyValuePair<Resort, int>(resort, 3));
        }

        [TestMethod]
        public void AddReportEntryDoesAsExpected2()
        {
            Resort resort = new Resort()
            {
                Id = 8,
                Name = "Hotel Chino",
                TouristPointId = 3,
                Reservations = new List<Reservation>()
                {
                    CreateAcceptedReservation(new DateTime(2020,10,14), new DateTime(2020,11,13)),
                    CreateCreatedReservation(new DateTime(2020,10,3), new DateTime(2020,10,5)),
                    CreateWaitingForPaymentReservation(new DateTime(2020,7,20), new DateTime(2020,8,10))
                }
            };
            ReservationReport report = new ReservationReport()
            {
                Input = new ReservationReportInput()
                {
                    InitialDate = new DateTime(2020, 10, 6),
                    FinalDate = new DateTime(2020, 10, 13),
                    TouristPointId = 3
                }
            };

            report.AddReportEntry(resort);
            CollectionAssert.Contains(report.ReservationPerResort, new KeyValuePair<Resort, int>(resort, 0));
        }

        [TestMethod]
        public void AddReportEntryDoesAsExpected3()
        {
            Resort resort = new Resort()
            {
                Id = 7,
                Name = "Hotel Aleman",
                TouristPointId = 2,
                Reservations = new List<Reservation>()
                {
                    CreateDeniedReservation(new DateTime(2020,10,10), new DateTime(2020,11,13)),
                    CreateExpiredReservation(new DateTime(2020,10,6), new DateTime(2020,10,14)),
                    CreateExpiredReservation(new DateTime(2020,10,10), new DateTime(2020,10,13))
                }
            };
            ReservationReport report = new ReservationReport()
            {
                Input = new ReservationReportInput()
                {
                    InitialDate = new DateTime(2020, 10, 6),
                    FinalDate = new DateTime(2020, 10, 13),
                    TouristPointId = 2
                }
            };

            report.AddReportEntry(resort);
            CollectionAssert.Contains(report.ReservationPerResort, new KeyValuePair<Resort, int>(resort, 0));
        }

        [TestMethod]
        public void AddReportEntryDoesAsExpected4()
        {
            Resort resort = new Resort()
            {
                Id = 7,
                Name = "Hotel Peruano",
                TouristPointId = 2,
                Reservations = new List<Reservation>()
                {
                    CreateDeniedReservation(new DateTime(2020,10,10), new DateTime(2020,11,13)),
                    CreateExpiredReservation(new DateTime(2020,10,6), new DateTime(2020,10,14)),
                    CreateAcceptedReservation(new DateTime(2020,10,10), new DateTime(2020,11,13)),
                    CreateWaitingForPaymentReservation(new DateTime(2020,10,6), new DateTime(2020,10,14)),
                    CreateCreatedReservation(new DateTime(2020,7,20), new DateTime(2020,8,10))
                }
            };
            ReservationReport report = new ReservationReport()
            {
                Input = new ReservationReportInput()
                {
                    InitialDate = new DateTime(2020, 10, 6),
                    FinalDate = new DateTime(2020, 10, 13),
                    TouristPointId = 2
                }
            };

            report.AddReportEntry(resort);
            CollectionAssert.Contains(report.ReservationPerResort, new KeyValuePair<Resort, int>(resort, 2));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddReportEntryWithInvalidTouristPointId()
        {
            Resort resort = new Resort()
            {
                TouristPointId = 13,
            };

            ReservationReport report = new ReservationReport()
            {
                Input = new ReservationReportInput()
                {
                    TouristPointId = 2
                }
            };

            report.AddReportEntry(resort);
        }

        [TestMethod]
        public void RemoveEntriesWithNoReservationsDoesAsExpected()
        {
            ReservationReport report = new ReservationReport();
            report.ReservationPerResort.Add(new KeyValuePair<Resort, int>(new Resort() { Id = 3 }, 0));
            report.ReservationPerResort.Add(new KeyValuePair<Resort, int>(new Resort() { Id = 5 }, 2));
            report.ReservationPerResort.Add(new KeyValuePair<Resort, int>(new Resort() { Id = 8 }, 7));
            report.ReservationPerResort.Add(new KeyValuePair<Resort, int>(new Resort() { Id = 19 }, 0));

            report.RemoveEntriesWithNoReservations();
            Assert.IsTrue(report.ReservationPerResort.TrueForAll(entry => entry.Value != 0));
        }

        [TestMethod]
        public void SortReportEntriesDoesAsExpected()
        {
            ReservationReport report = new ReservationReport();
            LoadReservationsPerResort();

            _reservationsPerResort.ForEach(entry => report.ReservationPerResort.Add(entry));
            report.SortReportEntries();

            CollectionAssert.AreEqual(_sortedReservationsPerResort, report.ReservationPerResort);
        }

        [TestMethod]
        public void ReportFailedExpectingTrue()
        {
            ReservationReport report = new ReservationReport();

            Assert.IsTrue(report.ReportFailed());
        }

        [TestMethod]
        public void ReportFailedExpectingFalse()
        {
            ReservationReport report = new ReservationReport();
            report.ReservationPerResort.Add(new KeyValuePair<Resort, int>(new Resort() { Id = 3 }, 4));

            Assert.IsFalse(report.ReportFailed());
        }

        #region Helpers
        private void LoadReservationsPerResort()
        {
            KeyValuePair<Resort, int> entry1 = new KeyValuePair<Resort, int>(new Resort() { Id = 19, MemberSince = new DateTime(2020, 11, 13) }, 6);
            KeyValuePair<Resort, int> entry2 = new KeyValuePair<Resort, int>(new Resort() { Id = 5, MemberSince = new DateTime(2020, 3, 12) }, 2);
            KeyValuePair<Resort, int> entry3 = new KeyValuePair<Resort, int>(new Resort() { Id = 8, MemberSince = new DateTime(2020, 11, 13) }, 7);
            KeyValuePair<Resort, int> entry4 = new KeyValuePair<Resort, int>(new Resort() { Id = 2, MemberSince = new DateTime(2020, 11, 12) }, 6);

            _reservationsPerResort.Add(entry1);
            _reservationsPerResort.Add(entry2);
            _reservationsPerResort.Add(entry3);
            _reservationsPerResort.Add(entry4);

            _sortedReservationsPerResort.Add(entry3);
            _sortedReservationsPerResort.Add(entry4);
            _sortedReservationsPerResort.Add(entry1);
            _sortedReservationsPerResort.Add(entry2);
        }
        private Reservation CreateAcceptedReservation(DateTime checkIn, DateTime checkOut)
        {
            Reservation reservation = new Reservation() 
            {
                Accommodation = new Accommodation()
                {
                    CheckIn = checkIn,
                    CheckOut = checkOut
                }
            };
            reservation.UpdateState(new ReservationState() { State = PossibleReservationStates.Accepted.ToString() });

            return reservation;
        }
        private Reservation CreateWaitingForPaymentReservation(DateTime checkIn, DateTime checkOut)
        {
            Reservation reservation = new Reservation()
            {
                Accommodation = new Accommodation()
                {
                    CheckIn = checkIn,
                    CheckOut = checkOut
                }
            };
            reservation.UpdateState(new ReservationState() { State = PossibleReservationStates.WaitingForPayment.ToString() });

            return reservation;
        }
        private Reservation CreateCreatedReservation(DateTime checkIn, DateTime checkOut)
        {
            Reservation reservation = new Reservation()
            {
                Accommodation = new Accommodation()
                {
                    CheckIn = checkIn,
                    CheckOut = checkOut
                }
            };
            reservation.UpdateState(new ReservationState() { State = PossibleReservationStates.Created.ToString() });

            return reservation;
        }
        private Reservation CreateDeniedReservation(DateTime checkIn, DateTime checkOut)
        {
            Reservation reservation = new Reservation()
            {
                Accommodation = new Accommodation()
                {
                    CheckIn = checkIn,
                    CheckOut = checkOut
                }
            };
            reservation.UpdateState(new ReservationState() { State = PossibleReservationStates.Denied.ToString() });

            return reservation;
        }
        private Reservation CreateExpiredReservation(DateTime checkIn, DateTime checkOut)
        {
            Reservation reservation = new Reservation()
            {
                Accommodation = new Accommodation()
                {
                    CheckIn = checkIn,
                    CheckOut = checkOut
                }
            };
            reservation.UpdateState(new ReservationState() { State = PossibleReservationStates.Expired.ToString() });

            return reservation;
        }
        #endregion
    }
}
