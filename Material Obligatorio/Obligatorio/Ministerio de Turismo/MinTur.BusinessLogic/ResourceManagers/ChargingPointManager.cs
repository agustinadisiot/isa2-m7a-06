using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;

namespace MinTur.BusinessLogic.ResourceManagers
{
    public class ChargingPointManager : IChargingPointManager
    {
        private readonly IRepositoryFacade _repositoryFacade;

        public ChargingPointManager(IRepositoryFacade repositoryFacade)
        {
            _repositoryFacade = repositoryFacade;
        }

        public ChargingPoint DeleteChargingPoint(int id)
        {
            return _repositoryFacade.DeleteChargingPointById(id);
        }

        public List<ChargingPoint> GetAllChargingPoints()
        {
            return _repositoryFacade.GetAllChargingPoints();
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

            storedTouristPoint.ValidateId();
            return storedTouristPoint;
        }
    }
}