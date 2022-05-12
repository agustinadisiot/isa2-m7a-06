using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.Reports;
using MinTur.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinTur.Domain.Test.Reports
{
    [TestClass]
    public class ReservationReportInputTest
    {
        [TestMethod]
        public void ValidInputPassesValidation()
        {
            ReservationReportInput input = new ReservationReportInput()
            {
                InitialDate = new DateTime(2020, 10, 10),
                FinalDate = new DateTime(2020, 11, 13),
                TouristPointId = 3
            };

            input.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void FinalDateEqualInitialDateFailsValidation()
        {
            ReservationReportInput input = new ReservationReportInput()
            {
                InitialDate = new DateTime(2020, 11, 13),
                FinalDate = new DateTime(2020, 11, 13),
                TouristPointId = 3
            };

            input.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void FinalDateBeforeInitialDateFailsValidation()
        {
            ReservationReportInput input = new ReservationReportInput()
            {
                InitialDate = new DateTime(2020, 11, 13),
                FinalDate = new DateTime(2020, 10, 13),
                TouristPointId = 3
            };

            input.ValidOrFail();
        }

    }
}
