using MinTur.Exceptions;
using System;

namespace MinTur.Domain.Reports
{
    public class ReservationReportInput
    {
        public int TouristPointId { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        
        public virtual void ValidOrFail()
        {
            ValidateDates();
        }

        private void ValidateDates()
        {
            if(InitialDate == null || FinalDate == null)
                throw new InvalidRequestDataException("Must provide valid initial & final dates");

            if (FinalDate <= InitialDate)
                throw new InvalidRequestDataException("Final date must be greater than initial date");
        }
    }
}
