using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;

namespace MinTur.ServiceRegistration.Test.Dummies
{
    public class RepositoryFacadeDummy : IRepositoryFacade
    {
        public Guid CreateNewAuthorizationTokenFor(Administrator administrator)
        {
            throw new NotImplementedException();
        }

        public void DeleteResort(Resort resort)
        {
            throw new NotImplementedException();
        }

        public void DeleteAdministratorById(int administratorId)
        {
            throw new NotImplementedException();
        }

        public List<Administrator> GetAllAdministrators()
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public List<Region> GetAllRegions()
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetAllReservations()
        {
            throw new NotImplementedException();
        }

        public List<Resort> GetAllResorts()
        {
            throw new NotImplementedException();
        }

        public AuthorizationToken GetAuthenticationTokenById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Region GetRegionById(int id)
        {
            throw new NotImplementedException();
        }

        public Reservation GetReservationById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Resort GetResortById(int resortId)
        {
            throw new NotImplementedException();
        }

        public List<Resort> GetTouristPointAvailableResorts(int touristPointId)
        {
            throw new NotImplementedException();
        }

        public TouristPoint GetTouristPointById(int touristPointId)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Guid StoreReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public int StoreResort(Resort resort)
        {
            throw new NotImplementedException();
        }

        public int StoreTouristPoint(TouristPoint touristPoint)
        {
            throw new NotImplementedException();
        }

        public void UpdateResortAvailability(int resortId, bool newAvailability)
        {
            throw new NotImplementedException();
        }
        public void UpdateReservationState(Guid reservationId, ReservationState reservationState)
        {
            throw new NotImplementedException();
        }

        public int StoreAdministrator(Administrator administrator)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdministrator(Administrator administrator)
        {
            throw new NotImplementedException();
        }
        public Administrator GetAdministratorById(int administratorId)
        {
            throw new NotImplementedException();
        }

        public List<TouristPoint> GetAllTouristPointsByMatchingCriteria(Func<TouristPoint, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public List<Resort> GetAllResortsByMatchingCriteria(Func<Resort, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public void UpdateResort(Resort resort)
        {
            throw new NotImplementedException();
        }

        public int StoreReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Review GetReviewById(int reviewId)
        {
            throw new NotImplementedException();
        }

        public int StoreChargingPoint(ChargingPoint chargingPoint)
        {
            throw new NotImplementedException();
        }

        public ChargingPoint GetChargingPointById(int chargingPointId)
        {
            throw new NotImplementedException();
        }
    }
}
