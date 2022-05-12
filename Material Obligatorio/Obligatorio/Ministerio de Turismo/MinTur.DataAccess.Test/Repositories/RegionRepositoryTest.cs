using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MinTur.DataAccess.Contexts;
using MinTur.Domain.BusinessEntities;
using MinTur.DataAccess.Repositories;
using System.Linq;
using MinTur.Exceptions;

namespace MinTur.DataAccess.Test.Repositories
{
    [TestClass]
    public class RegionRepositoryTest
    {
        private RegionRepository _repository;
        private NaturalUruguayContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.Memory);
            _repository = new RegionRepository(_context);
        }

        [TestCleanup]
        public void CleanUp() 
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllRegionsOnEmptyRepository()
        {
            List<Region> expectedRegions = new List<Region>();
            List<Region> retrievedRegions = _repository.GetAllRegions();

            CollectionAssert.AreEquivalent(expectedRegions, retrievedRegions);
        }

        [TestMethod]
        public void GetAllRegionsReturnsAsExpected()
        {
            List<Region> expectedRegions = new List<Region>();
            LoadRegions(expectedRegions);

            List<Region> retrievedRegions = _repository.GetAllRegions();
            CollectionAssert.AreEquivalent(expectedRegions, retrievedRegions);
        }

        [TestMethod]
        public void GetAllRegionsIncludesRelatedEntities()
        {
            Region expectedRegion = CreateRegionWithRelatedTouristPoint();

            List<Region> retrievedRegions = _repository.GetAllRegions();
            Assert.IsTrue(retrievedRegions.Contains(expectedRegion));
            Assert.IsTrue(retrievedRegions.Any(r => r.TouristPoints.Equals(expectedRegion.TouristPoints)));
        }

        [TestMethod]
        public void GetRegionByIdReturnsAsExpected()
        {
            Region expectedRegion = CreateRegionWithRelatedTouristPoint();
            Region retrievedRegion = _repository.GetRegionById(expectedRegion.Id);

            Assert.IsTrue(expectedRegion.Equals(retrievedRegion));
        }

        [TestMethod]
        public void GetRegionByIdIncludesRelatedEntities()
        {
            Region expectedRegion = CreateRegionWithRelatedTouristPoint();
            Region retrievedRegion = _repository.GetRegionById(expectedRegion.Id);

            Assert.IsTrue(expectedRegion.Equals(retrievedRegion));
            Assert.IsTrue(retrievedRegion.TouristPoints.Equals(expectedRegion.TouristPoints));
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetRegionByIdWhichDoesntExistThrowsException()
        {
            Region retrievedRegion = _repository.GetRegionById(-3);
        }

        #region Helpers
        private void LoadRegions(List<Region> regions)
        {
            Region region1 = new Region() { Id = 1, Name = "Metropolitana" };
            Region region2 = new Region() { Id = 2, Name = "Centro Sur" };
            Region region3 = new Region() { Id = 3, Name = "Litoral Norte" };
            Region region4 = new Region() { Id = 4, Name = "Corredor Pájaros Pintados" };

            regions.Add(region1);
            regions.Add(region2);
            regions.Add(region3);
            regions.Add(region4);

            _context.Regions.Add(region1);
            _context.Regions.Add(region2);
            _context.Regions.Add(region3);
            _context.Regions.Add(region4);
            _context.SaveChanges();
        }
        private Region CreateRegionWithRelatedTouristPoint()
        {
            Region region = new Region() { Id = 1, Name = "Metropolitana" };
            Category category1 = new Category() { Id = 1, Name = "Playas" };
            Category category2 = new Category() { Id = 2, Name = "Ciudades" };

            TouristPoint touristPoint1 = new TouristPoint()
            {
                Id = 1,
                Name = "Punta del Este",
                Region = region,
                RegionId = region.Id,
                Image = new Image() { Data = "sadfsdaff" }
            };
            touristPoint1.AddCategory(category1);

            TouristPoint touristPoint2 = new TouristPoint()
            {
                Id = 2,
                Name = "Cabo Polonio",
                Region = region,
                RegionId = region.Id,
                Image = new Image() { Data = "qwdwqd" }
            };
            touristPoint2.AddCategory(category2);

            region.TouristPoints.Add(touristPoint1);
            region.TouristPoints.Add(touristPoint2);

            _context.Regions.Add(region);
            _context.Categories.Add(category1);
            _context.Categories.Add(category2);
            _context.TouristPoints.Add(touristPoint1);
            _context.TouristPoints.Add(touristPoint2);
            _context.SaveChanges();

            return region;
        }   
        #endregion

    }
}