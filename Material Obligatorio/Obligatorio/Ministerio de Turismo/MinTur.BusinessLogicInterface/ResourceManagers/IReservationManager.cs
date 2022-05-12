using System;
using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.Reports;

namespace MinTur.BusinessLogicInterface.ResourceManagers
{
    public interface IReservationManager
    {
        Reservation GetReservationById(Guid reservationId);
        List<Reservation> GetAllReservations();
        Reservation RegisterReservation(Reservation reservation);
        Reservation UpdateReservationState(Guid reservationId, ReservationState newState);
        ReservationReport GenerateReservationReport(ReservationReportInput input);
    }
}