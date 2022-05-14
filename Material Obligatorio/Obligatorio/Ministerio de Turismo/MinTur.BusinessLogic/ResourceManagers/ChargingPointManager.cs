using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using System;

namespace MinTur.BusinessLogic.ResourceManagers
{
    public class ChargingPointManager : IChargingPointManager
    {
        private readonly IRepositoryFacade _repositoryFacade;

        public ChargingPointManager(IRepositoryFacade repositoryFacade)
        {
            _repositoryFacade = repositoryFacade;
        }
        
        public ChargingPoint GetChargingPointById(int chargingPoint)
        {
            return _repositoryFacade.GetChargingPointById(chargingPoint);
        }

        public ChargingPoint RegisterChargingPoint(ChargingPoint chargingPoint)
        {
            chargingPoint.ValidOrFail();

            int newChargingPointId = _repositoryFacade.StoreChargingPoint(chargingPoint);
            ChargingPoint storedTouristPoint = _repositoryFacade.GetChargingPointById(newChargingPointId);

            return storedTouristPoint;
        }
    }
}