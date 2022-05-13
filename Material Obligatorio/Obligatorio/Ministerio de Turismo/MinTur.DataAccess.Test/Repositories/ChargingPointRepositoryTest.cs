using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.DataAccess.Contexts;
using MinTur.Domain.BusinessEntities;
using System.Linq;

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
        public void StoreChargingPointReturnsAsExpected() 
        {
            ChargingPoint chargingPoint = LoadRelatedEntitiesAndCreateTouristPoint();
            int newChargingPointId = _repository.ChargingPointRepository(chargingPoint);

            Assert.AreEqual(chargingPoint.Id, newChargingPointId);
            Assert.IsNotNull(_context.ChargingPoints.Where(t => t.Id == newChargingPointId).FirstOrDefault());
        }


    }
}