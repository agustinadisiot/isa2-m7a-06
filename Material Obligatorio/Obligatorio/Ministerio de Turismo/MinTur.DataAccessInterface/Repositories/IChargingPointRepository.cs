using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;

namespace MinTur.DataAccessInterface.Repositories
{
    public interface IChargingPointRepository
    {
        int StoreChargingPoint(ChargingPoint chargingPoint);
        ChargingPoint GetChargingPointById(int chargingPointId);
    }
}
