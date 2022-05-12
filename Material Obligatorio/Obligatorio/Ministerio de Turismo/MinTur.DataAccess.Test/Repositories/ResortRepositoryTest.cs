using MinTur.DataAccess.Contexts;
using MinTur.DataAccess.Repositories;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.DataAccess.Test.Repositories
{
    [TestClass]
    public class ResortRepositoryTest
    {
        private ResortRepository _repository;
        private NaturalUruguayContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.Memory);
            _repository = new ResortRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllResortsByMatchingCriteriaReturnsAsExpected()
        {
            List<Resort> expectedResorts = new List<Resort>();
            LoadResorts(expectedResorts);

            List<Resort> retrievedResorts = _repository.GetAllResortsByMatchingCriteria(r => true);
            CollectionAssert.AreEquivalent(expectedResorts, retrievedResorts);
        }

        [TestMethod]
        public void GetAllResortsByMatchingCriteriaIncludesRelatedEntities()
        {
            List<Resort> expectedResorts = new List<Resort>();
            LoadResorts(expectedResorts);

            List<Resort> retrievedResorts = _repository.GetAllResortsByMatchingCriteria(r => true);
            CollectionAssert.AreEquivalent(expectedResorts, retrievedResorts);
            Assert.IsTrue(retrievedResorts.All(r => r.Images != null));
            Assert.IsTrue(retrievedResorts.All(r => r.Images.All(i => i.Data != null)));
            Assert.IsTrue(retrievedResorts.All(r => r.TouristPoint != null));
        }

        [TestMethod]
        public void GetResortByIdReturnsAsExpected()
        {
            Resort expectedResort = CreateResort();
            _context.Set<Resort>().Add(expectedResort);
            _context.SaveChanges();

            Resort retrievedResort = _repository.GetResortById(expectedResort.Id);
            Assert.IsTrue(expectedResort.Equals(retrievedResort));
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetResortByIdDoesntExist()
        {
            _repository.GetResortById(-47);
        }

        [TestMethod]
        public void GetResortByIdIncludesRelatedEntities()
        {
            Resort expectedResort = CreateResort();
            _context.Set<Resort>().Add(expectedResort);
            _context.SaveChanges();

            Resort retrievedResort = _repository.GetResortById(expectedResort.Id);
            Assert.IsTrue(expectedResort.Equals(retrievedResort));
            Assert.IsTrue(retrievedResort.Images != null);
            Assert.IsTrue(retrievedResort.TouristPoint != null);
        }

        [TestMethod]
        public void UpdateResortReturnsAsExpected()
        {
            bool newAvailability = false;
            Resort resort = CreateResort();
            _context.Set<Resort>().Add(resort);
            _context.SaveChanges();
            _context.Entry(resort).State = EntityState.Detached;

            resort.Available = newAvailability;
            _repository.UpdateResort(resort);
            Resort updatedResort = _repository.GetResortById(resort.Id);
            Assert.IsFalse(updatedResort.Available);
        }

        public void StorResortReturnsAsExpected()
        {
            Resort resort = CreateResort();
            _context.Set<TouristPoint>().Add(resort.TouristPoint);
            _context.SaveChanges();

            int newResortId = _repository.StoreResort(resort);
            Assert.AreEqual(resort.Id, newResortId);
            Assert.IsNotNull(_context.Resorts.Where(r => r.Id == newResortId).FirstOrDefault());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void UpdateResortDoesntExist()
        {
            _repository.UpdateResort(new Resort() { Id = -4 });
        }
        public void StoreResortInvalidTouristPoint()
        {
            Resort resort = CreateResort();
            resort.TouristPointId = -78;

            _repository.StoreResort(resort);
        }

        [TestMethod]
        public void DeleteResortReturnsAsExpected()
        {
            Resort resortToBeDeleted = CreateResort();
            _context.Set<TouristPoint>().Add(resortToBeDeleted.TouristPoint);
            _context.Set<Resort>().Add(resortToBeDeleted);
            _context.SaveChanges();

            _repository.DeleteResort(resortToBeDeleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteResortDoesntExist()
        {
            _repository.DeleteResort(new Resort() { Id = -47 });
        }

        #region Helpers
        private Resort CreateResort()
        {
            return new Resort()
            {
                Id = 3,
                Address = "Direccion",
                Description = "Descripcion ....",
                Name = "Hotel Italiano",
                PricePerNight = 520,
                Stars = 4,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                TouristPointId = 2
            };
        }

        private void LoadResorts(List<Resort> resorts)
        {
            Resort resort1 = new Resort()
            {
                Id = 3,
                Name = "Hotel Italiano",
                Description = "Hotel lindo con estilo italinao",
                Address = "Calle en PDE",
                PhoneNumber = "1849848",
                PricePerNight = 100,
                Stars = 4,
                ReservationMessage = "Gracias por quere venir :)",
                TouristPointId = 2,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del Este"
                }
            };
            resort1.Images.Add(new Image() { Data = "adsojkfiew" });
            resort1.Images.Add(new Image() { Data = "gkorkgork" });
            Resort resort2 = new Resort()
            {
                Id = 4,
                Name = "Hotel argentino",
                Description = "Buen hotel",
                Address = "Calle en Piriapolis",
                PhoneNumber = "18432239848",
                PricePerNight = 400,
                Stars = 5,
                ReservationMessage = "Gracias por quere venir amigos",
                TouristPointId = 3,
                TouristPoint = new TouristPoint()
                {
                    Id = 3,
                    Name = "Piriapolis"
                }
            };
            resort2.Images.Add(new Image() { Data = "adsdas" });

            resorts.Add(resort1);
            resorts.Add(resort2);

            _context.Resorts.Add(resort1);
            _context.Resorts.Add(resort2);
            _context.SaveChanges();
        }

        #endregion
    }
}
