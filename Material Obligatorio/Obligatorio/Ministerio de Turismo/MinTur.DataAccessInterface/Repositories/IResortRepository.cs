using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;

namespace MinTur.DataAccessInterface.Repositories
{
    public interface IResortRepository
    {
        List<Resort> GetAllResortsByMatchingCriteria(Func<Resort, bool> criteria);
        Resort GetResortById(int resortId);
        void UpdateResort(Resort resort);
        int StoreResort(Resort resort);
        void DeleteResort(Resort resort);
    }
}
