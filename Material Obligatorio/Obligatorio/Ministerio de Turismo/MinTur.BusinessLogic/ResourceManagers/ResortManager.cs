using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.SearchCriteria;
using System;
using System.Collections.Generic;

namespace MinTur.BusinessLogic.ResourceManagers
{
    public class ResortManager : IResortManager
    {
        private readonly IRepositoryFacade _repositoryFacade;

        public ResortManager(IRepositoryFacade repositoryFacade)
        {
            _repositoryFacade = repositoryFacade;
        }

        public List<Resort> GetAllResortsByMatchingCriteria(ISearchCriteria<Resort> searchCriteria)
        {
            List<Resort> retrievedResorts = _repositoryFacade.GetAllResortsByMatchingCriteria(searchCriteria.MatchesCriteria);
            return retrievedResorts;
        }

        public List<Resort> GetAllResortsForAccommodationByMatchingCriteria(Accommodation accommodation, ISearchCriteria<Resort> searchCriteria)
        {
            accommodation.ValidOrFail(DateTime.Today);
            List<Resort> retrievedResorts = _repositoryFacade.GetAllResortsByMatchingCriteria(searchCriteria.MatchesCriteria);

            return retrievedResorts;
        }

        public void DeleteResortById(int resortId)
        {
            Resort resortToBeDeleted = _repositoryFacade.GetResortById(resortId);
            _repositoryFacade.DeleteResort(resortToBeDeleted);
        }

        public Resort GetResortById(int resortId)
        {
            return _repositoryFacade.GetResortById(resortId);
        }

        public Resort UpdateResortAvailability(int resortId, bool newAvailability)
        {
            Resort retrievedResort = _repositoryFacade.GetResortById(resortId);

            retrievedResort.Available = newAvailability;
            _repositoryFacade.UpdateResort(retrievedResort);
            
            return retrievedResort;
        }
        public Resort RegisterResort(Resort resort)
        {
            resort.ValidOrFail();

            TouristPoint touristPoint = _repositoryFacade.GetTouristPointById(resort.TouristPointId);
            resort.TouristPoint = touristPoint;

            int newResortId = _repositoryFacade.StoreResort(resort);
            Resort createdResort = _repositoryFacade.GetResortById(newResortId);

            return createdResort;
        }

    }
}
