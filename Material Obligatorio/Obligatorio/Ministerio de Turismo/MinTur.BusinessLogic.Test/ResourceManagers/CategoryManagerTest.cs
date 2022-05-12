using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MinTur.BusinessLogic.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;

namespace MinTur.BusinessLogic.Test.ResourceManagers
{
    [TestClass]
    public class CategoryManagerTest
    {
        private List<Category> _categories;
        private Mock<IRepositoryFacade> _repositoryFacadeMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _categories = new List<Category>();
            _repositoryFacadeMock = new Mock<IRepositoryFacade>(MockBehavior.Strict);

            LoadCategories();
        }

        private void LoadCategories()
        {
            Category category1 = new Category() { Id = 1, Name = "Ciudades" };
            Category category2 = new Category() { Id = 2, Name = "Pueblos" };
            _categories.Add(category1);
            _categories.Add(category2);
        }

        #endregion


        [TestMethod]
        public void GetAllCategoriesReturnsAsExpected()
        {
            _repositoryFacadeMock.Setup(r => r.GetAllCategories()).Returns(_categories);

            CategoryManager categoryManager = new CategoryManager(_repositoryFacadeMock.Object);
            List<Category> retrievedCategories = categoryManager.GetAllCategories();

            _repositoryFacadeMock.VerifyAll();
            CollectionAssert.AreEquivalent(retrievedCategories, _categories);
        }

        [TestMethod]
        public void GetCategoryByIdReturnsAsExpected()
        {
            Category categoryToFind = new Category() { Id = 1, Name = "Ciudades" };
            _repositoryFacadeMock.Setup(r => r.GetCategoryById(categoryToFind.Id)).Returns(categoryToFind);

            CategoryManager categoryManager = new CategoryManager(_repositoryFacadeMock.Object);
            Category retrievedCategory = categoryManager.GetCategoryById(categoryToFind.Id);

            _repositoryFacadeMock.VerifyAll();
            Assert.IsTrue(retrievedCategory.Equals(categoryToFind));
        }


    }
}