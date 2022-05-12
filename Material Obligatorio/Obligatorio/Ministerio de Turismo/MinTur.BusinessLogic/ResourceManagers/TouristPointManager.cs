using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.SearchCriteria;
using System;
using System.Collections.Generic;

namespace MinTur.BusinessLogic.ResourceManagers
{
    public class TouristPointManager : ITouristPointManager
    {
        private readonly IRepositoryFacade _repositoryFacade;

        public TouristPointManager(IRepositoryFacade repositoryFacade)
        {
            _repositoryFacade = repositoryFacade;
        }

        public List<TouristPoint> GetAllTouristPointsByMatchingCriteria(ISearchCriteria<TouristPoint> searchCriteria)
        {
            return _repositoryFacade.GetAllTouristPointsByMatchingCriteria(searchCriteria.MatchesCriteria);
        }

        public TouristPoint GetTouristPointById(int touristPointId)
        {
            return _repositoryFacade.GetTouristPointById(touristPointId);
        }

        public TouristPoint RegisterTouristPoint(TouristPoint touristPoint)
        {
            touristPoint.ValidOrFail();

            int newTouristPointId = _repositoryFacade.StoreTouristPoint(touristPoint);
            TouristPoint storedTouristPoint = _repositoryFacade.GetTouristPointById(newTouristPointId);

            return storedTouristPoint;
        }
    }
}