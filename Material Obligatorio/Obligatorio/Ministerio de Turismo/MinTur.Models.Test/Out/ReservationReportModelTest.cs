using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MinTur.Models.Out;
using MinTur.Domain.Reports;
using System.Linq;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class ReservationReportModelTest
    {
        [TestMethod]
        public void ReservationReportModelGetsCreatedAsExpected()
        {
            ReservationReport report = new ReservationReport();
            report.ReservationPerResort.Add(new KeyValuePair<Resort, int>(new Resort() { Name = "Hotel Italiano" }, 2));
            report.ReservationPerResort.Add(new KeyValuePair<Resort, int>(new Resort() { Name = "Hotel Aleman" }, 6));

            List<ReservationIndividualReportModel> individualReportsModel = report.ReservationPerResort.Select(r => new ReservationIndividualReportModel(r)).ToList();

            Assert.IsNotNull(individualReportsModel.Find(r => r.ResortName == "Hotel Italiano" && r.AmountOfReservations == 2));
            Assert.IsNotNull(individualReportsModel.Find(r => r.ResortName == "Hotel Aleman" && r.AmountOfReservations == 6));

        }
    }
}