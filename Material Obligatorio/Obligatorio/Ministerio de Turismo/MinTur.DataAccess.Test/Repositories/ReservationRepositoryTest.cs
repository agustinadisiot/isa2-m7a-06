using System;
using System.Collections.Generic;
using System.Linq;
using MinTur.DataAccess.Contexts;
using MinTur.DataAccess.Repositories;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MinTur.DataAccess.Test.Repositories
{
    [TestClass]
    public class ReservationRepositoryTest
    {
        private ReservationRepository _repository;

        private NaturalUruguayContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.Memory);
            _repository = new ReservationRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllReservationsOnEmptyRepository()
        {
            List<Reservation> expectedReservations = new List<Reservation>();
            List<Reservation> retrievedReservations = _repository.GetAllReservations();

            CollectionAssert.AreEquivalent(expectedReservations, retrievedReservations);
        }

        [TestMethod]
        public void GetAllReservationsReturnsAsExpected()
        {
            List<Reservation> expectedReservations = new List<Reservation>();
            LoadReservations(expectedReservations);

            List<Reservation> retrievedReservations = _repository.GetAllReservations();
            CollectionAssert.AreEquivalent(expectedReservations, retrievedReservations);
        }

        [TestMethod]
        public void GetReservationByIdReturnsAsExpected()
        {
            Reservation expectedReservation = new Reservation()
            {
                Resort = new Resort() { Id = 7 },
                Accommodation = new Accommodation() { Id = 3 }
            };
            _context.Reservations.Add(expectedReservation);
            _context.SaveChanges();

            Reservation retrievedReservation = _repository.GetReservationById(expectedReservation.Id);
            Assert.IsTrue(expectedReservation.Equals(retrievedReservation));
        }

        [TestMethod]
        public void GetReservationByIdIncludesRelatedEntities()
        {
            Reservation expectedReservation = new Reservation()
            {
                Resort = new Resort() { Id = 7},
                Accommodation = new Accommodation() { Id = 3 }
            };
            _context.Reservations.Add(expectedReservation);
            _context.SaveChanges();

            Reservation retrievedReservation = _repository.GetReservationById(expectedReservation.Id);
            Assert.IsTrue(expectedReservation.Equals(retrievedReservation));
            Assert.IsNotNull(expectedReservation.Resort);
            Assert.IsNotNull(expectedReservation.Accommodation);
            Assert.IsNotNull(expectedReservation.Accommodation.Guests);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetReservationByIdWhichDoesntExistThrowsException()
        {
            _repository.GetReservationById(new Guid());
        }

        [TestMethod]
        public void StoreReservationReturnsAsExpected()
        {
            Reservation reservation = CreateReservationWithSpecificId(Guid.NewGuid());
            _context.Set<Resort>().Add(reservation.Resort);
            _context.SaveChanges();
            _context.Entry(reservation.Resort).State = EntityState.Detached;

            Guid newReservationId = _repository.StoreReservation(reservation);
            Assert.AreEqual(reservation.Id, newReservationId);
            Assert.IsNotNull(_context.Reservations.Where(r => r.Id == newReservationId).FirstOrDefault());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void StoreReservationInvalidResort()
        {
            Reservation reservation = CreateReservationWithSpecificId(Guid.NewGuid());
            reservation.Resort.Id = -78;

            _repository.StoreReservation(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StoreReservationResortNotAvailable()
        {
            Resort relatedResort = CreateResort();
            relatedResort.Available = false;
            _context.Set<Resort>().Add(relatedResort);
            _context.SaveChanges();
            _context.Entry(relatedResort).State = EntityState.Detached;

            Reservation reservation = CreateReservationWithSpecificId(Guid.NewGuid());
            reservation.Resort = relatedResort;
            _repository.StoreReservation(reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void UpdateReservationWhichDoesntExistOnDb() 
        {
            ReservationState newState = new ReservationState() { Description = "New description", State = "Accepted" };
            _repository.UpdateReservationState(Guid.NewGuid(), newState);
        }

        [TestMethod]
        public void UpdateReservationUpdatesDb()
        {
            Reservation reservation = CreateReservationWithSpecificId(Guid.NewGuid());
            _context.ReservationStates.Add(reservation.ActualState);
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            _context.Entry(reservation).State = EntityState.Detached;
            _context.Entry(reservation.ActualState).State = EntityState.Detached;

            ReservationState newState = new ReservationState() { Description = "New description", State = "Accepted" };
            _repository.UpdateReservationState(reservation.Id, newState);

            Reservation retrievedReservation = _context.Reservations.Include(r => r.ActualState).FirstOrDefault();
            Assert.IsNotNull(retrievedReservation);
            Assert.AreEqual(newState.Description, retrievedReservation.ActualState.Description);
            Assert.AreEqual(newState.State, retrievedReservation.ActualState.State);
        }

        #region Helpers
        private void LoadReservations(List<Reservation> reservations)
        {
            Reservation reservation1 = CreateReservationWithSpecificId(Guid.NewGuid());
            Reservation reservation2 = CreateReservationWithSpecificId(Guid.NewGuid());
            reservations.Add(reservation1);
            reservations.Add(reservation2);
            _context.Reservations.Add(reservation1);
            _context.Reservations.Add(reservation2);
            _context.SaveChanges();
        }
        private Reservation CreateReservationWithSpecificId(Guid reservationId)
        {
            Reservation reservation = new Reservation()
            {
                Name = "Pedro",
                Surname = "Perez",
                Email = "pedroPerez@gmail.com",
                TotalPrice = 500,
                Accommodation = new Accommodation()
                {
                    CheckIn = new DateTime(2020, 9, 29),
                    CheckOut = new DateTime(2020, 9, 29)
                },
                Resort = new Resort()
                {
                    Name = "Hotel Italiano"
                }
            };
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Adult.ToString(), Amount = 3 });
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Kid.ToString(), Amount = 1 });
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Baby.ToString(), Amount = 9 });
            reservation.Accommodation.Guests.Add(new GuestGroup() { GuestGroupType = GuestType.Retired.ToString(), Amount = 2 });

            return reservation;
        }
        private Resort CreateResort()
        {
            return new Resort()
            {
                Id = 3,
                Address = "Direccion",
                Description = "Descripcion ....",
                Name = "Hotel Italiano",
                PricePerNight = 520,
                Stars = 4,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                TouristPointId = 2
            };
        }
        #endregion

    }
}