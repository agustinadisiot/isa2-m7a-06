using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;

namespace MinTur.BusinessLogic.ResourceManagers
{
    public class RegionManager : IRegionManager
    {
        private readonly IRepositoryFacade _repositoryFacade;

        public RegionManager(IRepositoryFacade repositoryFacade) 
        {
            _repositoryFacade = repositoryFacade;
        }

        public List<Region> GetAllRegions()
        {
            return _repositoryFacade.GetAllRegions();
        }

        public Region GetRegionById(int regionId)
        {
            return _repositoryFacade.GetRegionById(regionId);
        }

    }
}
