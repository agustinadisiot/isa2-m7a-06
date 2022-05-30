using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.SearchCriteria;

namespace MinTur.BusinessLogicInterface.ResourceManagers
{
    public interface IChargingPointManager
    {
        ChargingPoint RegisterChargingPoint(ChargingPoint chargingPoint);
        ChargingPoint GetChargingPointById(int chargingPoint);
        ChargingPoint DeleteChargingPoint(int id);
        List<ChargingPoint> GetAllChargingPoints();
    }
}