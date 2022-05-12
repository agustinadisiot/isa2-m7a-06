using MinTur.Domain.BusinessEntities;
using MinTur.Domain.SearchCriteria;
using System.Collections.Generic;

namespace MinTur.BusinessLogicInterface.ResourceManagers
{
    public interface IRegionManager
    {
        List<Region> GetAllRegions();
        Region GetRegionById(int regionId);
    }
}
