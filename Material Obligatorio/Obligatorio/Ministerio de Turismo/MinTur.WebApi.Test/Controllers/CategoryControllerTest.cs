using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using MinTur.Models.Out;
using System.Collections.Generic;
using MinTur.WebApi.Controllers;

namespace MinTur.WebApi.Test.Controllers
{
    [TestClass]
    public class CategoryControllerTest
    {
        private List<Category> _categories;
        private List<CategoryBasicInfoModel> _categoriesModel;
        private Mock<ICategoryManager> _categoryManagerMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _categories = new List<Category>();
            _categoriesModel = new List<CategoryBasicInfoModel>();
            _categoryManagerMock = new Mock<ICategoryManager>(MockBehavior.Strict);

            LoadCategories();
            LoadCategoriesModels();
        }

        private void LoadCategories()
        {
            Category category1 = new Category() { Id = 1, Name = "Ciudades" };
            Category category2 = new Category() { Id = 2, Name = "Pueblos" };
            _categories.Add(category1);
            _categories.Add(category2);
        }


        private void LoadCategoriesModels()
        {
            foreach (Category category in _categories)
            {
                _categoriesModel.Add(new CategoryBasicInfoModel(category));
            }
        }

        #endregion

        [TestMethod]
        public void GetAllCategoriesOkTest()
        {
            _categoryManagerMock.Setup(c => c.GetAllCategories()).Returns(_categories);
            CategoryController categoriesController = new CategoryController(_categoryManagerMock.Object);

            IActionResult result = categoriesController.GetAll();
            OkObjectResult okResult = result as OkObjectResult;
            List<CategoryBasicInfoModel> responseModel = okResult.Value as List<CategoryBasicInfoModel>;

            _categoryManagerMock.VerifyAll();
            CollectionAssert.AreEquivalent(responseModel, _categoriesModel);
        }

        [TestMethod]
        public void GetSpecificCategoryOKTest()
        {
            Category returnedCategory = new Category() { Id = 1, Name = "Ciudades" };

            _categoryManagerMock.Setup(c => c.GetCategoryById(It.IsAny<int>())).Returns(returnedCategory);
            CategoryController categoriesController = new CategoryController(_categoryManagerMock.Object);

            IActionResult result = categoriesController.GetSpecificCategory(returnedCategory.Id);
            OkObjectResult okResult = result as OkObjectResult;
            CategoryBasicInfoModel responseModel = okResult.Value as CategoryBasicInfoModel;

            _categoryManagerMock.VerifyAll();
            Assert.IsTrue(responseModel.Equals(new CategoryBasicInfoModel(returnedCategory)));
        }

    }
}