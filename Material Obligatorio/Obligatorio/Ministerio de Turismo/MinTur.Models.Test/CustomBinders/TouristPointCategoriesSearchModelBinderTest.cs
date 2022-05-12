using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.CustomBinders;
using MinTur.Models.In;
using Moq;
using System.Threading.Tasks;

namespace MinTur.Models.Test.CustomBinders
{
    [TestClass]
    public class TouristPointCategoriesSearchModelBinderTest
    {
        private Mock<ModelBindingContext> _mockedContext;
        private TouristPointCategoriesSearchModelBinder _modelbinder;

        [TestInitialize]
        public void SetUp()
        {
            _modelbinder = new TouristPointCategoriesSearchModelBinder();
            _mockedContext = new Mock<ModelBindingContext>(MockBehavior.Strict);
        }

        [TestMethod]
        public void ModelGetsCreatedSuccesfullyFromCorrectQueryAllProperties()
        {
            _mockedContext.SetupAllProperties();
            _mockedContext.Setup(c => c.ValueProvider.GetValue("categoriesId")).Returns(new ValueProviderResult("7-3"));
            _mockedContext.Setup(c => c.ValueProvider.GetValue("regionId")).Returns(new ValueProviderResult("9"));

            TouristPointSearchModel expectedModel = new TouristPointSearchModel();
            expectedModel.CategoriesId.Add(7);
            expectedModel.CategoriesId.Add(3);
            expectedModel.RegionId = 9;
            _modelbinder.BindModelAsync(_mockedContext.Object);

            _mockedContext.VerifyAll();
            Assert.IsTrue(_mockedContext.Object.Result.Model.Equals(expectedModel));
        }


        [TestMethod]
        public void ModelGetsCreatedSuccesfullyFromCorrectQueryWithRegionIdButNoCategories()
        {
            _mockedContext.SetupAllProperties();
            _mockedContext.Setup(c => c.ValueProvider.GetValue("categoriesId")).Returns(new ValueProviderResult());
            _mockedContext.Setup(c => c.ValueProvider.GetValue("regionId")).Returns(new ValueProviderResult("3"));

            TouristPointSearchModel expectedModel = new TouristPointSearchModel();
            expectedModel.RegionId = 3;
            _modelbinder.BindModelAsync(_mockedContext.Object);

            _mockedContext.VerifyAll();
            Assert.IsTrue(_mockedContext.Object.Result.Model.Equals(expectedModel));
        }

        [TestMethod]
        public void ModelGetsCreatedSuccesfullyFromCorrectQueryWithCategoriesButNoRegionId()
        {
            _mockedContext.SetupAllProperties();
            _mockedContext.Setup(c => c.ValueProvider.GetValue("categoriesId")).Returns(new ValueProviderResult("7-3"));
            _mockedContext.Setup(c => c.ValueProvider.GetValue("regionId")).Returns(new ValueProviderResult());

            TouristPointSearchModel expectedModel = new TouristPointSearchModel();
            expectedModel.CategoriesId.Add(7);
            expectedModel.CategoriesId.Add(3);
            _modelbinder.BindModelAsync(_mockedContext.Object);

            _mockedContext.VerifyAll();
            Assert.IsTrue(_mockedContext.Object.Result.Model.Equals(expectedModel));
        }

        [TestMethod]
        public void ModelGetsCreatedSuccesfullyFromCorrectQueryWithNoProperties()
        {
            _mockedContext.SetupAllProperties();
            _mockedContext.Setup(c => c.ValueProvider.GetValue("categoriesId")).Returns(new ValueProviderResult());
            _mockedContext.Setup(c => c.ValueProvider.GetValue("regionId")).Returns(new ValueProviderResult());

            TouristPointSearchModel expectedModel = new TouristPointSearchModel();
            _modelbinder.BindModelAsync(_mockedContext.Object);

            _mockedContext.VerifyAll();
            Assert.IsTrue(_mockedContext.Object.Result.Model.Equals(expectedModel));
        }

        [TestMethod]
        public void ModelDoesntGetCreatedBecauseOfInvalidCategoriesIdRegex()
        {
            _mockedContext.SetupAllProperties();
            _mockedContext.Setup(c => c.ValueProvider.GetValue("categoriesId")).Returns(new ValueProviderResult("4-5pedro-78"));
            _mockedContext.Setup(c => c.ValueProvider.GetValue("regionId")).Returns(new ValueProviderResult("5"));
            _mockedContext.Setup(c => c.ModelState).Returns(new ModelStateDictionary());

            _modelbinder.BindModelAsync(_mockedContext.Object);

            _mockedContext.VerifyAll();
            Assert.IsFalse(_mockedContext.Object.Result.IsModelSet);
            Assert.IsTrue(_mockedContext.Object.ModelState.ErrorCount > 0);
        }

        [TestMethod]
        public void ModelDoesntGetCreatedBecauseOfInvalidRegionIdFormat()
        {
            _mockedContext.SetupAllProperties();
            _mockedContext.Setup(c => c.ValueProvider.GetValue("categoriesId")).Returns(new ValueProviderResult("1-2"));
            _mockedContext.Setup(c => c.ValueProvider.GetValue("regionId")).Returns(new ValueProviderResult("fefwqfewf"));
            _mockedContext.Setup(c => c.ModelState).Returns(new ModelStateDictionary());

            _modelbinder.BindModelAsync(_mockedContext.Object);

            _mockedContext.VerifyAll();
            Assert.IsFalse(_mockedContext.Object.Result.IsModelSet);
            Assert.IsTrue(_mockedContext.Object.ModelState.ErrorCount > 0);
        }

    }
}

