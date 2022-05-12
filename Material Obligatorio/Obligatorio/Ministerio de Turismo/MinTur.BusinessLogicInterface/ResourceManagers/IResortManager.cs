using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.SearchCriteria;

namespace MinTur.BusinessLogicInterface.ResourceManagers
{
    public interface IResortManager
    {
        Resort GetResortById(int resortId);
        List<Resort> GetAllResortsByMatchingCriteria(ISearchCriteria<Resort> searchCriteria);
        List<Resort> GetAllResortsForAccommodationByMatchingCriteria(Accommodation accommodation, ISearchCriteria<Resort> searchCriteria);
        Resort UpdateResortAvailability(int resortId, bool newAvailability);
        Resort RegisterResort(Resort resort);
        void DeleteResortById(int resortId);
    }
}
