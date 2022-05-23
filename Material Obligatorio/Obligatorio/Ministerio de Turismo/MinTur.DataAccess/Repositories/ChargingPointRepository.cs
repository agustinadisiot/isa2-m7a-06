using System.Collections.Generic;
using System.Linq;
using MinTur.DataAccessInterface.Repositories;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;

namespace MinTur.DataAccess.Repositories
{
    public class ChargingPointRepository : IChargingPointRepository
    {
        protected DbContext Context { get; set; }

        public ChargingPointRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public ChargingPoint GetChargingPointById(int chargingPointId)
        {
            if (!ChargingPointExists(chargingPointId))
                throw new ResourceNotFoundException("Could not find specified charging point");

            ChargingPoint retrievedChargingPoint = Context.Set<ChargingPoint>().AsNoTracking().Where(t => t.Id == chargingPointId).Include(t => t.Region)
                .FirstOrDefault();

            return retrievedChargingPoint;
        }
        
        private bool ChargingPointExists(int chargingPointId)
        {
            ChargingPoint chargingPoint = Context.Set<ChargingPoint>().AsNoTracking().Where(t => t.Id == chargingPointId).FirstOrDefault();
            return chargingPoint != null;
        }

        public int StoreChargingPoint(ChargingPoint chargingPoint)
        {
            if (!RegionExists(chargingPoint.RegionId))
                throw new ResourceNotFoundException("Could not find specified region");

            chargingPoint.Region = Context.Set<Region>().Where(r => r.Id == chargingPoint.RegionId).FirstOrDefault();
            StoreChargingPointInDb(chargingPoint);

            return chargingPoint.Id;
        }

        private void StoreChargingPointInDb(ChargingPoint chargingPoint)
        {
            Context.Entry(chargingPoint.Region).State = EntityState.Unchanged;

            Context.Set<ChargingPoint>().Add(chargingPoint);
            Context.SaveChanges();

            Context.Entry(chargingPoint.Region).State = EntityState.Detached;
        }

        private bool RegionExists(int regionId)
        {
            Region region = Context.Set<Region>().AsNoTracking().Where(r => r.Id == regionId).FirstOrDefault();
            return region != null;
        }

        public ChargingPoint DeleteChargingPointById(int chargingPointId)
        {
            ChargingPoint existingPoint = GetChargingPointById(chargingPointId);
            Context.Set<ChargingPoint>().Remove(existingPoint);
            Context.SaveChanges();
            return new ChargingPoint();
        }
    }
}