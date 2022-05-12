using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.SearchCriteria;

namespace MinTur.BusinessLogicInterface.ResourceManagers
{
    public interface ITouristPointManager
    {
        TouristPoint GetTouristPointById(int touristPointId);
        TouristPoint RegisterTouristPoint(TouristPoint touristPoint);
        List<TouristPoint> GetAllTouristPointsByMatchingCriteria(ISearchCriteria<TouristPoint> searchCriteria);
    }
}