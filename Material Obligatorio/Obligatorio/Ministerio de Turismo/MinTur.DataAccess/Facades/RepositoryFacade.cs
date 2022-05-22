using Microsoft.EntityFrameworkCore;
using MinTur.DataAccess.Repositories;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;

namespace MinTur.DataAccess.Facades
{
    [ExcludeFromCodeCoverage]
    public class RepositoryFacade : IRepositoryFacade
    {
        private readonly DbContext _context;
        private RegionRepository _regionRepository;
        private TouristPointRepository _touristPointRepository;
        private CategoryRepository _categoryRepository;
        private ResortRepository _resortRepository;
        private ReservationRepository _reservationRepository;
        private AuthenticationTokenRepository _authenticationTokenRepository;
        private AdministratorRepository _administratorRepository;
        private ReviewRepository _reviewRepository;
        private ChargingPointRepository _chargingPointRepository;

        public RepositoryFacade(DbContext context)
        {
            _context = context;
            _regionRepository = new RegionRepository(_context);
            _touristPointRepository = new TouristPointRepository(_context);
            _categoryRepository = new CategoryRepository(_context);
            _resortRepository = new ResortRepository(_context);
            _reservationRepository = new ReservationRepository(_context);
            _authenticationTokenRepository = new AuthenticationTokenRepository(_context);
            _administratorRepository = new AdministratorRepository(_context);
            _reviewRepository = new ReviewRepository(_context);
            _chargingPointRepository = new ChargingPointRepository(_context);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categoryRepository.GetCategoryById(categoryId);
        }

        public List<Region> GetAllRegions()
        {
            return _regionRepository.GetAllRegions();
        }

        public Region GetRegionById(int regionId)
        {
            return _regionRepository.GetRegionById(regionId);
        }

        public Resort GetResortById(int resortId)
        {
            return _resortRepository.GetResortById(resortId);
        }

        public Guid StoreReservation(Reservation reservation)
        {
            return _reservationRepository.StoreReservation(reservation);
        }

        public Reservation GetReservationById(Guid id)
        {
            return _reservationRepository.GetReservationById(id);
        }

        public List<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAllReservations();
        }

        public TouristPoint GetTouristPointById(int touristPointId)
        {
            return _touristPointRepository.GetTouristPointById(touristPointId);
        }

        public AuthorizationToken GetAuthenticationTokenById(Guid id)
        {
            return _authenticationTokenRepository.GetAuthenticationTokenById(id);
        }

        public Guid CreateNewAuthorizationTokenFor(Administrator administrator)
        {
            return _authenticationTokenRepository.CreateNewAuthorizationTokenFor(administrator);
        }

        public int StoreTouristPoint(TouristPoint touristPoint)
        {
            return _touristPointRepository.StoreTouristPoint(touristPoint);
        }

        public List<Administrator> GetAllAdministrators()
        {
            return _administratorRepository.GetAllAdministrators();
        }

        public int StoreResort(Resort resort)
        {
            return _resortRepository.StoreResort(resort);
        }

        public void DeleteResort(Resort resort)
        {
            _resortRepository.DeleteResort(resort);
        }

        public void UpdateReservationState(Guid reservationId, ReservationState reservationState)
        {
            _reservationRepository.UpdateReservationState(reservationId, reservationState);
        }

        public void DeleteAdministratorById(int administratorId)
        {
            _administratorRepository.DeleteAdministratorById(administratorId);
        }

        public int StoreAdministrator(Administrator administrator)
        {
            return _administratorRepository.StoreAdministrator(administrator);
        }

        public void UpdateAdministrator(Administrator administrator)
        {
            _administratorRepository.UpdateAdministrator(administrator);
        }
        public Administrator GetAdministratorById(int administratorId)
        {
            return _administratorRepository.GetAdministratorById(administratorId);
        }

        public List<TouristPoint> GetAllTouristPointsByMatchingCriteria(Func<TouristPoint, bool> criteria)
        {
            return _touristPointRepository.GetAllTouristPointsByMatchingCriteria(criteria);
        }

        public List<Resort> GetAllResortsByMatchingCriteria(Func<Resort, bool> criteria)
        {
            return _resortRepository.GetAllResortsByMatchingCriteria(criteria);
        }

        public void UpdateResort(Resort resort)
        {
            _resortRepository.UpdateResort(resort);
        }

        public int StoreReview(Review review)
        {
            return _reviewRepository.StoreReview(review);
        }

        public Review GetReviewById(int reviewId)
        {
            return _reviewRepository.GetReviewById(reviewId);
        }

        public int StoreChargingPoint(ChargingPoint chargingPoint)
        {
            return _chargingPointRepository.StoreChargingPoint(chargingPoint);
        }

        public ChargingPoint GetChargingPointById(int chargingPointId)
        {
            return _chargingPointRepository.GetChargingPointById(chargingPointId);
        }

        public ChargingPoint DeleteChargingPointById(int chargingPointId)
        {
            return _chargingPointRepository.DeleteChargingPointById(chargingPointId);
        }
    }
}
