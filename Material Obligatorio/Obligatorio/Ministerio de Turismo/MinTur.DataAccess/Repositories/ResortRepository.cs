using MinTur.DataAccessInterface.Repositories;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MinTur.DataAccess.Repositories
{
    public class ResortRepository : IResortRepository
    {
        protected DbContext Context { get; set; }

        public ResortRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public List<Resort> GetAllResortsByMatchingCriteria(Func<Resort, bool> criteria)
        {
            return Context.Set<Resort>().AsNoTracking().Include(r => r.Images).Include(r => r.TouristPoint).Include(r => r.Reviews)
                .Include(r => r.Reservations).ThenInclude(r => r.Accommodation).Include(r => r.Reservations)
                .ThenInclude(r => r.ActualState).Where(criteria).ToList();
        }

        public Resort GetResortById(int resortId)
        {
            if (!ResortExists(resortId))
                throw new ResourceNotFoundException("Could not find specified resort");

            return Context.Set<Resort>().AsNoTracking().Where(r => r.Id == resortId).Include(r => r.Images).Include(r => r.TouristPoint)
                .Include(r => r.Reviews).Include(r => r.Reservations).ThenInclude(r => r.Accommodation)
                .Include(r => r.Reservations).ThenInclude(r => r.ActualState).FirstOrDefault();
        }

        public int StoreResort(Resort resort)
        {
            if (!TouristPointExists(resort.TouristPointId))
                throw new ResourceNotFoundException("Could not find specified touristPoint");

            StoreResortInDb(resort);
            return resort.Id;
        }

        public void DeleteResort(Resort resort)
        {
            if (!ResortExists(resort.Id))
                throw new ResourceNotFoundException("Could not find specified resort");

            DeleteResortFromDb(resort);
        }

        public void UpdateResort(Resort resort)
        {
            if (!ResortExists(resort.Id))
                throw new ResourceNotFoundException("Could not find specified resort");

            UpdateResortInDb(resort);
        }

        private bool ResortExists(int resortId)
        {
            Resort resort = Context.Set<Resort>().AsNoTracking().Where(r => r.Id == resortId).FirstOrDefault();
            return resort != null;
        }

        private bool TouristPointExists(int touristPointId)
        {
            TouristPoint touristPoint = Context.Set<TouristPoint>().AsNoTracking().Where(t => t.Id == touristPointId).FirstOrDefault();
            return touristPoint != null;
        }

        private void StoreResortInDb(Resort resort) 
        {
            Context.Entry(resort.TouristPoint).State = EntityState.Unchanged;
            Context.Set<Image>().AddRange(resort.Images);
            Context.Set<Resort>().Add(resort);
            Context.SaveChanges();
            Context.Entry(resort.TouristPoint).State = EntityState.Detached;
        }

        private void DeleteResortFromDb(Resort resort) 
        {
            Context.Entry(resort.TouristPoint).State = EntityState.Unchanged;
            Context.Set<Image>().RemoveRange(resort.Images);
            Context.Set<Resort>().Remove(resort);
            Context.SaveChanges();
            Context.Entry(resort.TouristPoint).State = EntityState.Detached;
        }

        private void UpdateResortInDb(Resort resort) 
        {
            Context.Entry(resort).State = EntityState.Modified;
            Context.SaveChanges();
            Context.Entry(resort).State = EntityState.Detached;
        }

    }
}
