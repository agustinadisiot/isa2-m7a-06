using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;

namespace MinTur.DataAccessInterface.Repositories
{
    public interface IRegionRepository
    {
        List<Region> GetAllRegions();
        Region GetRegionById(int regionId);
    }
}
