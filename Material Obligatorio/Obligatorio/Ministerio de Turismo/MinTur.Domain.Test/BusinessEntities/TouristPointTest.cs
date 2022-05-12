using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Domain.Test.BusinessEntities
{
    [TestClass]
    public class TouristPointTest
    {
        [TestMethod]
        public void TouristPointInitializedWithNoCategories()
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 1
            };
            List<TouristPointCategory> touristPointCategories = touristPoint.TouristPointCategory;

            Assert.IsTrue(touristPointCategories.Count == 0);
        }

        [TestMethod]
        public void TouristPointAddCategory()
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 1
            };
            Category newCategory = new Category { Id = 3, Name = "Pueblos" };
            touristPoint.AddCategory(newCategory);

            List<Category> categories = touristPoint.TouristPointCategory.Select(t => t.Category).ToList();
            Assert.IsTrue(categories.Contains(newCategory));
        }

        [TestMethod]
        public void TouristPointRemoveCategory()
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 1
            };
            Category newCategory = new Category { Id = 3, Name = "Pueblos" };
            touristPoint.AddCategory(newCategory);
            touristPoint.RemoveCategory(newCategory);

            List<Category> categories = touristPoint.TouristPointCategory.Select(t => t.Category).ToList();
            Assert.IsFalse(categories.Contains(newCategory));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void TouristPointWithInvalidNameFailsValidation() 
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Name = "Inva_lid! Name**",
                Description = "Valid description",
                Image = new Image() { Data = "iufhewuihgfew" },
                RegionId = 2
            };
            touristPoint.AddCategory(new Category() { Name = "Playas" });
            touristPoint.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void TouristPointWithLongerThanMaxDescriptionFailsValidation()
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Name = "Punta del Este",
                Description = new string('a',2001),
                Image = new Image() { Data = "iufhewuihgfew" },
                RegionId = 2
            };
            touristPoint.AddCategory(new Category() { Name = "Playas" });
            touristPoint.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void TouristPointWithoutImageFailsValidation()
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Name = "Punta del Este",
                Description = "Valid description",
                RegionId = 2
            };
            touristPoint.AddCategory(new Category() { Name = "Playas" });
            touristPoint.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void TouristPointWithInvalidImageFailsValidation()
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Name = "Punta del Este",
                Description = "Valid description",
                Image = new Image() { },
                RegionId = 2
            };
            touristPoint.AddCategory(new Category() { Name = "Playas" });
            touristPoint.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void TouristPointWithLessThanOneCategoryFailsValidation()
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Name = "Tacuarembó",
                Description = "Valid description",
                Image = new Image() { Data = "iufhewuihgfew" },
                RegionId = 2
            };
            touristPoint.ValidOrFail();
        }

        [TestMethod]
        public void ValidTouristPointPassesValidation()
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Name = "Tacuarembó",
                Description = "Valid description",
                Image = new Image() { Data = "iufhewuihgfew" },
                RegionId = 2
            };
            touristPoint.AddCategory(new Category() { Name = "Playas" });
            touristPoint.ValidOrFail();
        }
    }
}
