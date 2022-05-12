using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MinTur.DataAccess.Contexts;
using MinTur.Domain.BusinessEntities;
using MinTur.DataAccess.Repositories;
using System.Linq;
using MinTur.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MinTur.DataAccess.Test.Repositories
{
    [TestClass]
    public class TouristPointRepositoryTest
    {
        private TouristPointRepository _repository;
        private NaturalUruguayContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.Memory);
            _context.Database.EnsureDeleted();
            _repository = new TouristPointRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllTouristPointsByMatchingCriteriaReturnsAsExpected()
        {
            List<TouristPoint> expectedTouristPoints = new List<TouristPoint>();
            LoadTouristPoints(expectedTouristPoints);

            List<TouristPoint> retrievedTouristPoints = _repository.GetAllTouristPointsByMatchingCriteria(t => true);
            CollectionAssert.AreEquivalent(expectedTouristPoints, retrievedTouristPoints);
        }

        [TestMethod]
        public void GetAllTouristPointsByMatchingCriteriaIncludesEntitesRelated()
        {
            List<TouristPoint> expectedTouristPoints = new List<TouristPoint>();
            LoadTouristPoints(expectedTouristPoints);

            List<TouristPoint> retrievedTouristPoints = _repository.GetAllTouristPointsByMatchingCriteria(t => true);
            CollectionAssert.AreEquivalent(expectedTouristPoints, retrievedTouristPoints);
            foreach (TouristPoint retrievedTouristPoint in retrievedTouristPoints)
            {
                Assert.IsTrue(retrievedTouristPoint.TouristPointCategory != null);
                Assert.IsTrue(retrievedTouristPoint.Image != null);
                Assert.IsTrue(retrievedTouristPoint.TouristPointCategory.All(t => t.Category != null && t.TouristPoint != null));
                Assert.IsTrue(retrievedTouristPoint.Resorts != null);
                Assert.IsTrue(retrievedTouristPoint.Resorts.All(r => r.Images != null && r.TouristPoint != null));
            }
        }

        [TestMethod]
        public void GetTouristPointByIdReturnsAsExpected()
        {
            TouristPoint expectedTouristPoint = CreateTouristPoint();
            _context.TouristPoints.Add(expectedTouristPoint);
            _context.SaveChanges();

            TouristPoint retrievedTouristPoint = _repository.GetTouristPointById(expectedTouristPoint.Id);
            Assert.IsTrue(expectedTouristPoint.Equals(retrievedTouristPoint));
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetTouristPointByIdWhichDoesntExistThrowsException()
        {
            _repository.GetTouristPointById(-3);
        }

        [TestMethod]
        public void StoreTouristPointReturnsAsExpected() 
        {
            TouristPoint touristPoint = LoadRelatedEntitiesAndCreateTouristPoint();
            int newTouristPointId = _repository.StoreTouristPoint(touristPoint);

            Assert.AreEqual(touristPoint.Id, newTouristPointId);
            Assert.IsNotNull(_context.TouristPoints.Where(t => t.Id == newTouristPointId).FirstOrDefault());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void StoreTouristPointNonExistentRegion()
        {
            TouristPoint touristPoint = LoadRelatedEntitiesAndCreateTouristPoint();
            touristPoint.RegionId = -4;

            _repository.StoreTouristPoint(touristPoint);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void StoreTouristPointNonExistentCategory()
        {
            TouristPoint touristPoint = LoadRelatedEntitiesAndCreateTouristPoint();
            touristPoint.AddCategory(new Category() { Id = -5, Name = "InvalidCategory" });

            _repository.StoreTouristPoint(touristPoint);
        }


        #region Helpers
        public TouristPoint CreateTouristPoint()
        {
            return new TouristPoint()
            {
                Name = "Hotel Italiano",
                Description = "Descripcion sobre el hotel....",
                Image = new Image() { Data = "gfhewauihgeruig" },
                RegionId = 2,
                Region = new Region() { Id = 2, Name = "Metropolitana" },
            };
        }
        public TouristPoint LoadRelatedEntitiesAndCreateTouristPoint() 
        {
            Region region = new Region() { Name = "Metropolitana" };
            Category category1 = new Category() { Name = "Playas" };
            Category category2 = new Category() { Name = "Ciudades" };

            _context.Regions.Add(region);
            _context.Categories.Add(category1);
            _context.Categories.Add(category2);
            _context.SaveChanges();
            _context.Entry(region).State = EntityState.Detached;
            _context.Entry(category1).State = EntityState.Detached;
            _context.Entry(category2).State = EntityState.Detached;

            TouristPoint newTouristPoint =  new TouristPoint()
            {
                Name = "Hotel Italiano",
                Description = "Descripcion sobre el hotel....",
                Image = new Image() { Data = "gfhewauihgeruig" },
                RegionId = region.Id
            };
            newTouristPoint.AddCategory(category1);
            newTouristPoint.AddCategory(category2);

            return newTouristPoint;
        }
        private void LoadTouristPoints(List<TouristPoint> touristPoints)
        {
            Region region1 = new Region() { Name = "Metropolitana" };
            Region region2 = new Region() { Name = "Centro Sur" };
            Category category1 = new Category() { Name = "Playas" };
            Category category2 = new Category() { Name = "Ciudades" };

            TouristPoint touristPoint1 = new TouristPoint()
            {
                Name = "Punta Del Este",
                Description = "Donde el lujo y la naturaleza convergen: Punta del Este es reconocido internacionalmente como...",
                Image = new Image() { Id = 1, Data = "hjfduisahguf" },
                RegionId = region1.Id,
                Region = region1,
            };
            touristPoint1.AddCategory(category1);

            TouristPoint touristPoint2 = new TouristPoint()
            {
                Name = "Cabo Polonio",
                Description = "Donde el lujo y la naturaleza convergen: Punta del Este es reconocido internacionalmente como...",
                Image = new Image() { Id = 2, Data = "gfdhthtrjy" },
                RegionId = region2.Id,
                Region = region2,
            };
            touristPoint2.AddCategory(category2);

            Resort resort = new Resort()
            {
                Id = 3,
                Name = "Hotel Italiano",
                TouristPoint = touristPoint2,
                TouristPointId = touristPoint2.Id
            };
            resort.Images.Add(new Image() { Id = 3, Data = "uhfadsuhf" });
            touristPoint2.Resorts.Add(resort);

            touristPoints.Add(touristPoint1);
            touristPoints.Add(touristPoint2);

            _context.Regions.Add(region1);
            _context.Regions.Add(region2);
            _context.Categories.Add(category1);
            _context.Categories.Add(category2);
            _context.TouristPoints.Add(touristPoint1);
            _context.TouristPoints.Add(touristPoint2);
            _context.Resorts.Add(resort);
            _context.SaveChanges();
        }
        private void LoadResorts(TouristPoint relatedTouristPoint)
        {
            Resort resort1 = new Resort()
            {
                Id = 3,
                Address = "Direccion",
                Description = "Descripcion ....",
                Name = "Hotel Italiano",
                PricePerNight = 520,
                Stars = 4,
                TouristPoint = relatedTouristPoint,
                TouristPointId = relatedTouristPoint.Id
            };
            Resort resort2 = new Resort()
            {
                Id = 9,
                Address = "Direccion 2",
                Description = "Descripcion 2 ....",
                Name = "Hotel Aleman",
                PricePerNight = 330,
                Stars = 5,
                TouristPoint = relatedTouristPoint,
                TouristPointId = relatedTouristPoint.Id
            };
            relatedTouristPoint.Resorts.Add(resort1);
            relatedTouristPoint.Resorts.Add(resort2);
            resort1.Images.Add(new Image() { Id = 5, Data = "sdafasdgwe" });

            _context.TouristPoints.Add(relatedTouristPoint);
            _context.Resorts.Add(resort1);
            _context.Resorts.Add(resort2);
            _context.SaveChanges();
        }
        #endregion

    }
}