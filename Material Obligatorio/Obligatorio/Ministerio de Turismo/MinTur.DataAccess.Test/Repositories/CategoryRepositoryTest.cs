using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MinTur.DataAccess.Contexts;
using MinTur.Domain.BusinessEntities;
using MinTur.DataAccess.Repositories;
using MinTur.Exceptions;

namespace MinTur.DataAccess.Test.Repositories
{
    [TestClass]
    public class CategoryRepositoryTest
    {
        private CategoryRepository _repository;

        private NaturalUruguayContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.Memory);
            _repository = new CategoryRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllCategoriesOnEmptyRepository()
        {
            List<Category> expectedCategories = new List<Category>();
            List<Category> retrievedCategories = _repository.GetAllCategories();

            CollectionAssert.AreEquivalent(expectedCategories, retrievedCategories);
        }

        [TestMethod]
        public void GetAllCategoriesReturnsAsExpected()
        {
            List<Category> expectedCategories = new List<Category>();
            LoadCategories(expectedCategories);

            List<Category> retrievedCategories = _repository.GetAllCategories();
            CollectionAssert.AreEquivalent(expectedCategories, retrievedCategories);
        }

        [TestMethod]
        public void GetCategoryByIdReturnsAsExpected()
        {
            Category expectedCategory = new Category() { Id = 1, Name = "Ciudades" };
            _context.Categories.Add(expectedCategory);
            _context.SaveChanges();

            Category retrievedCategory = _repository.GetCategoryById(expectedCategory.Id);
            Assert.IsTrue(expectedCategory.Equals(retrievedCategory));
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetCategoryByIdWhichDoesntExistThrowsException()
        {
            _repository.GetCategoryById(-3);
        }

        #region Helpers
        private void LoadCategories(List<Category> categories)
        {
            Category category1 = new Category() { Id = 1, Name = "Ciudades" };
            Category category2 = new Category() { Id = 2, Name = "Pueblos" };

            categories.Add(category1);
            categories.Add(category2);

            _context.Categories.Add(category1);
            _context.Categories.Add(category2);
            _context.SaveChanges();
        }

        #endregion
    }
}