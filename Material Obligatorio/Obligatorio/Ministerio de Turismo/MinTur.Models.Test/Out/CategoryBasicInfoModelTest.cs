using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;
using System.Text;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class CategoryBasicInfoModelTest
    {
        [TestMethod]
        public void CategoryBasicInfoModelGetsCreatedAsExpected()
        {
            Category category = new Category { Id = 5, Name = "Metropolitana" };
            CategoryBasicInfoModel categoryBasicInfoModel = new CategoryBasicInfoModel(category);

            Assert.IsTrue(category.Id == categoryBasicInfoModel.Id);
            Assert.IsTrue(category.Name == categoryBasicInfoModel.Name);
        }

    }
}
