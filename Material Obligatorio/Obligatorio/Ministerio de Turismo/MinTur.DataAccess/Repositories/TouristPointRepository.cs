using System.Collections.Generic;
using System.Linq;
using MinTur.DataAccessInterface.Repositories;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;

namespace MinTur.DataAccess.Repositories
{
    public class TouristPointRepository : ITouristPointRepository
    {
        protected DbContext Context { get; set; }

        public TouristPointRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public List<TouristPoint> GetAllTouristPointsByMatchingCriteria(Func<TouristPoint, bool> criteria)
        {
            return Context.Set<TouristPoint>().AsNoTracking().Include(t => t.Region).Include(t => t.TouristPointCategory).ThenInclude(tc => tc.Category)
               .Include(t => t.Image).Include(t => t.Resorts).ThenInclude(r => r.Images).Where(criteria).ToList();
        }

        public TouristPoint GetTouristPointById(int touristPointId)
        {
            if (!TouristPointExists(touristPointId))
                throw new ResourceNotFoundException("Could not find specified tourist point");

            TouristPoint retrievedTouristPoint = Context.Set<TouristPoint>().AsNoTracking().Where(t => t.Id == touristPointId).Include(t => t.Region)
                .Include(t => t.TouristPointCategory).ThenInclude(tc => tc.Category).Include(t => t.Image)
                .Include(t => t.Resorts).ThenInclude(r => r.Images).FirstOrDefault();

            return retrievedTouristPoint;
        }

        public int StoreTouristPoint(TouristPoint touristPoint)
        {
            if (!RegionExists(touristPoint.RegionId))
                throw new ResourceNotFoundException("Could not find specified region");
            if (!(touristPoint.TouristPointCategory.All(tc => CategoryExists(tc.CategoryId))))
                throw new ResourceNotFoundException("Could not find specified category");

            touristPoint.Region = Context.Set<Region>().Where(r => r.Id == touristPoint.RegionId).FirstOrDefault();
            StoreTouristPointInDb(touristPoint);

            return touristPoint.Id;
        }

        private bool TouristPointExists(int touristPointId)
        {
            TouristPoint touristPoint = Context.Set<TouristPoint>().AsNoTracking().Where(t => t.Id == touristPointId).FirstOrDefault();
            return touristPoint != null;
        }

        private bool RegionExists(int regionId)
        {
            Region region = Context.Set<Region>().AsNoTracking().Where(r => r.Id == regionId).FirstOrDefault();
            return region != null;
        }

        private bool CategoryExists(int categoryId)
        {
            Category category = Context.Set<Category>().AsNoTracking().Where(c => c.Id == categoryId).FirstOrDefault();
            return category != null;
        }

        private void StoreTouristPointInDb(TouristPoint touristPoint) 
        {
            Context.Entry(touristPoint.Region).State = EntityState.Unchanged;
            touristPoint.TouristPointCategory.ForEach(t => Context.Entry(t.Category).State = EntityState.Unchanged);

            Context.Set<TouristPoint>().Add(touristPoint);
            Context.SaveChanges();

            Context.Entry(touristPoint.Region).State = EntityState.Detached;
            touristPoint.TouristPointCategory.ForEach(t => Context.Entry(t.Category).State = EntityState.Detached);
        }

    }
}