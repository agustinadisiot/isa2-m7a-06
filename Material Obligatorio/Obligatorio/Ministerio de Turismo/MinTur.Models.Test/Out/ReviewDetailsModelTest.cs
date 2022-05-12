using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.BusinessEntities;
using MinTur.Models.Out;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class ReviewDetailsModelTest
    {
        [TestMethod]
        public void ReviewDetailsModelGetsCreatedAsExpected() 
        {
            Review review = new Review()
            {
                Id = 4,
                Stars = 3,
                Text = "Ni muy muy ni tan tan",
                Name = "Pepe",
                Surname = "Pepitos",
                ResortId = 6
            };
            ReviewDetailsModel reviewDetailsModel = new ReviewDetailsModel(review);

            Assert.AreEqual(review.Id, reviewDetailsModel.Id);
            Assert.AreEqual(review.Stars, reviewDetailsModel.Stars);
            Assert.AreEqual(review.Text, reviewDetailsModel.Text);
            Assert.AreEqual(review.Name, reviewDetailsModel.Name);
            Assert.AreEqual(review.Surname, reviewDetailsModel.Surname);
            Assert.AreEqual(review.ResortId, reviewDetailsModel.ResortId);
        }

    }
}
