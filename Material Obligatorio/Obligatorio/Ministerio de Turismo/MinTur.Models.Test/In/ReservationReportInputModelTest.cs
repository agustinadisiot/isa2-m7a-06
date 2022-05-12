using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.Reports;
using MinTur.Models.In;
using System;

namespace MinTur.Models.Test.In
{
    [TestClass]
    public class ReservationReportInputModelTest
    {
        [TestMethod]
        public void ToEntityReturnsAsExpected()
        {
            ReservationReportInputModel model = new ReservationReportInputModel()
            {
                InitialDate = new DateTime(2020, 10, 10),
                FinalDate = new DateTime(2020, 11, 13),
                TouristPointId = 3
            };
            ReservationReportInput reservationReportInput = model.ToEntity();

            Assert.AreEqual(model.InitialDate, reservationReportInput.InitialDate);
            Assert.AreEqual(model.FinalDate, reservationReportInput.FinalDate);
            Assert.AreEqual(model.TouristPointId, reservationReportInput.TouristPointId);
        }
    }
}
