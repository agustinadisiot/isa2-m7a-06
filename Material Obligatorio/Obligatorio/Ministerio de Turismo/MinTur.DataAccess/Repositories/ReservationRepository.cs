using Microsoft.EntityFrameworkCore;
using MinTur.DataAccessInterface.Repositories;
using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;
using System.Linq;
using MinTur.Exceptions;
using System;

namespace MinTur.DataAccess.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        protected DbContext Context { get; set; }

        public ReservationRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public Reservation GetReservationById(Guid reservationId)
        {
            if (!ReservationExists(reservationId))
                throw new ResourceNotFoundException("Could not find specified reservation");

            Reservation retrievedReservation = Context.Set<Reservation>().AsNoTracking().Include(r => r.Resort).Include(r => r.Accommodation)
                .ThenInclude(a => a.Guests).Include(r => r.ActualState).Where(c => c.Id == reservationId).FirstOrDefault();

            return retrievedReservation;
        }

        public List<Reservation> GetAllReservations()
        {
            return Context.Set<Reservation>().AsNoTracking().Include(r => r.Accommodation).ThenInclude(a => a.Guests).Include(r => r.Resort)
                .Include(r => r.ActualState).ToList();
        }

        public Guid StoreReservation(Reservation reservation)
        {
            if (!ResortExists(reservation.Resort.Id))
                throw new ResourceNotFoundException("Could not find specified resort");
            if (!ResortIsAvailable(reservation.Resort.Id))
                throw new InvalidOperationException("Sorry, but this resort is no longer available");

            StoreReservationInDb(reservation);

            return reservation.Id;
        }
        public void UpdateReservationState(Guid reservationId, ReservationState reservationState)
        {
            if (!ReservationExists(reservationId))
                throw new ResourceNotFoundException("Could not find specified reservation");

            Reservation retrievedReservation = Context.Set<Reservation>().Include(r => r.ActualState).Where(r => r.Id == reservationId).FirstOrDefault();

            retrievedReservation.UpdateState(reservationState);
            UpdateReservationStateInDb(retrievedReservation);
        }

        private bool ResortExists(int resortId)
        {
            Resort resort = Context.Set<Resort>().AsNoTracking().Where(r => r.Id == resortId).FirstOrDefault();
            return resort != null;
        }

        private bool ResortIsAvailable(int resortId)
        {
            Resort resort = Context.Set<Resort>().AsNoTracking().Where(r => r.Id == resortId).FirstOrDefault();
            return resort != null && resort.Available;
        }

        private bool ReservationExists(Guid reservationId)
        {
            Reservation reservation = Context.Set<Reservation>().AsNoTracking().Where(r => r.Id == reservationId).FirstOrDefault();
            return reservation != null;
        }

        private void StoreReservationInDb(Reservation reservation) 
        {
            Context.Entry(reservation.Resort).State = EntityState.Unchanged;

            Context.Set<GuestGroup>().AddRange(reservation.Accommodation.Guests);
            Context.Set<Accommodation>().Add(reservation.Accommodation);
            Context.Set<ReservationState>().Add(reservation.ActualState);
            Context.Set<Reservation>().Add(reservation);

            Context.SaveChanges();
            Context.Entry(reservation.Resort).State = EntityState.Detached;
        }

        private void UpdateReservationStateInDb(Reservation reservation)
        {
            Context.Entry(reservation.ActualState).State = EntityState.Modified;
            Context.SaveChanges();

            Context.Entry(reservation).State = EntityState.Detached;
            Context.Entry(reservation.ActualState).State = EntityState.Detached;
        }
    }
}