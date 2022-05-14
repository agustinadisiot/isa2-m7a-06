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

        public TouristPoint RegisterTouristPoint(TouristPoint touristPoint)
        {
            touristPoint.ValidOrFail();

            int newTouristPointId = _repositoryFacade.StoreTouristPoint(touristPoint);
            TouristPoint storedTouristPoint = _repositoryFacade.GetTouristPointById(newTouristPointId);

            return storedTouristPoint;
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