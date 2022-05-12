using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;

namespace MinTur.DataAccessInterface.Repositories
{
    public interface ITouristPointRepository
    {
        TouristPoint GetTouristPointById(int touristPointId);
        int StoreTouristPoint(TouristPoint touristPoint);
        List<TouristPoint> GetAllTouristPointsByMatchingCriteria(Func<TouristPoint, bool> criteria);
    }
}
