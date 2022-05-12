using System;
using System.Collections.Generic;
using MinTur.BusinessLogicInterface.Pricing;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.Reports;

namespace MinTur.BusinessLogic.ResourceManagers
{
    public class ReservationManager : IReservationManager
    {
        private readonly IRepositoryFacade _repositoryFacade;
        private readonly IResortPricingCalculator _resortPricingCalculator;

        public ReservationManager(IRepositoryFacade repositoryFacade, IResortPricingCalculator resortPricingCalculator)
        {
            _repositoryFacade = repositoryFacade;
            _resortPricingCalculator = resortPricingCalculator;
        }

        public List<Reservation> GetAllReservations()
        {
            return _repositoryFacade.GetAllReservations();
        }

        public Reservation GetReservationById(Guid reservationId)
        {
            return _repositoryFacade.GetReservationById(reservationId);
        }

        public Reservation RegisterReservation(Reservation reservation)
        {
            reservation.ValidOrFail(DateTime.Today);
            Resort relatedResort = _repositoryFacade.GetResortById(reservation.Resort.Id);
            reservation.TotalPrice = _resortPricingCalculator.CalculateTotalPriceForAccommodation(relatedResort, reservation.Accommodation);

            Guid newReservationId = _repositoryFacade.StoreReservation(reservation);
            Reservation createdReservation = _repositoryFacade.GetReservationById(newReservationId);

            return createdReservation;
        }

        public Reservation UpdateReservationState(Guid reservationId, ReservationState newState)
        {
            newState.ValidOrFail();
            _repositoryFacade.UpdateReservationState(reservationId, newState);
            Reservation updatedReservation = _repositoryFacade.GetReservationById(reservationId);

            return updatedReservation;
        }

        public ReservationReport GenerateReservationReport(ReservationReportInput input)
        {
            TouristPoint retrievedTouristPoint = _repositoryFacade.GetTouristPointById(input.TouristPointId);
            input.ValidOrFail();

            ReservationReport report = ComputeReport(input, retrievedTouristPoint);

            if (report.ReportFailed())
                throw new InvalidOperationException("The report failed because there are no existing reservations in here");

            return report;
        }

        private ReservationReport ComputeReport(ReservationReportInput input, TouristPoint touristPoint)
        {
            ReservationReport report = new ReservationReport()
            {
                Input = input
            };

            foreach (Resort resort in touristPoint.Resorts)
            {
                Resort retrievedResort = _repositoryFacade.GetResortById(resort.Id);
                report.AddReportEntry(retrievedResort);
            }

            report.RemoveEntriesWithNoReservations();
            report.SortReportEntries();

            return report;
        }
    }
}