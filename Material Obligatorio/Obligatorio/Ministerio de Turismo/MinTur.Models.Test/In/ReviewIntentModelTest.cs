using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.BusinessEntities;
using MinTur.Models.In;
using System;

namespace MinTur.Models.Test.In
{
    [TestClass]
    public class ReviewIntentModelTest
    {
        [TestMethod]
        public void ToEntityReturnsAsExpected()
        {
            ReviewIntentModel reviewIntentModel = new ReviewIntentModel()
            {
                ReservationId = Guid.NewGuid(),
                Stars = 2,
                Text = "Maso maso, el hotel es mas feo que en las fotos"
            };
            Review review = reviewIntentModel.ToEntity();

            Assert.AreEqual(reviewIntentModel.Stars, review.Stars);
            Assert.AreEqual(reviewIntentModel.Text, review.Text);
        }
    }
}
