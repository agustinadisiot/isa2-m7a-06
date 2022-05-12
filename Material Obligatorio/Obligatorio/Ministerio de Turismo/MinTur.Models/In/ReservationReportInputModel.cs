using MinTur.Domain.Reports;
using System;

namespace MinTur.Models.In
{
    public class ReservationReportInputModel
    {
        public int TouristPointId { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
    
        public ReservationReportInput ToEntity()
        {
            return new ReservationReportInput()
            {
                InitialDate = InitialDate,
                FinalDate = FinalDate,
                TouristPointId = TouristPointId
            };
        }
    }
}
