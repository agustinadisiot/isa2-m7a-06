using System;
using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;

namespace MinTur.DataAccessInterface.Repositories
{
    public interface IReservationRepository
    {
        Reservation GetReservationById(Guid reservationId);
        List<Reservation> GetAllReservations();
        Guid StoreReservation(Reservation reservation);
        void UpdateReservationState(Guid reservationId, ReservationState reservationState);
    }
}