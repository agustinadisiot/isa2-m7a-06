using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.DataAccess.Contexts;
using MinTur.Domain.BusinessEntities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MinTur.DataAccess.Repositories;
using MinTur.Exceptions;

namespace MinTur.DataAccess.Test.Repositories
{
    [TestClass]
    public class ChargingPointRepositoryTest
    {
        private ChargingPointRepository _repository;
        private NaturalUruguayContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.Memory);
            _context.Database.EnsureDeleted();
            _repository = new ChargingPointRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
        
        [TestMethod]
        public void GetChargingPointByIdReturnsAsExpected()
        {
            ChargingPoint expectedChargingPoint = LoadRelatedEntitiesAndCreateTouristPoint();
            _context.ChargingPoints.Add(expectedChargingPoint);
            _context.SaveChanges();

            ChargingPoint retrievedChargingPoint = _repository.GetChargingPointById(expectedChargingPoint.Id);
            Assert.IsTrue(expectedChargingPoint.Id.Equals(retrievedChargingPoint.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetTChargingPointByIdWhichDoesntExistThrowsException()
        {
            _repository.GetChargingPointById(-3);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void StoreTouristPointNonExistentRegion()
        {
            ChargingPoint chargingPoint = LoadRelatedEntitiesAndCreateTouristPoint();
            chargingPoint.RegionId = -4;

            _repository.StoreChargingPoint(chargingPoint);
        }

        [TestMethod]
        public void StoreChargingPointReturnsAsExpected() 
        {
            ChargingPoint chargingPoint = LoadRelatedEntitiesAndCreateTouristPoint();
            int newChargingPointId = _repository.StoreChargingPoint(chargingPoint);

            Assert.AreEqual(chargingPoint.Id, newChargingPointId);
            Assert.IsNotNull(_context.ChargingPoints.Where(t => t.Id == newChargingPointId).FirstOrDefault());
        }


        
        #region Helpers

        private ChargingPoint LoadRelatedEntitiesAndCreateTouristPoint() 
        {
            Region region = new Region() { Name = "Metropolitana" , Id = 10};

            _context.Regions.Add(region);
            _context.SaveChanges();
            _context.Entry(region).State = EntityState.Detached;
            ChargingPoint newChargingPoint =  new ChargingPoint()
            {
                Name = "Ancap Rambla Gandhi",
                Description = "Descripcion sobre la estacion de carga....",
                RegionId = region.Id,
                Address = "Gandhi 2647",
                Id = 1234
            };

            return newChargingPoint;
        }
        
        #endregion
        
    }
}