using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;

namespace MinTur.Domain.Test.BusinessEntities
{
    [TestClass]
    public class ReviewTest
    {
        [TestMethod]
        public void ValidReviewPassesTest()
        {
            Review review = new Review()
            {
                Name = "Pedro",
                Surname = "Perez",
                Stars = 3,
                Text = "text"
            };

            review.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ReviewWithInvalidNameFailsValidation()
        {
            Review review = new Review()
            {
                Name = "Pedro!$",
                Surname = "Perez",
                Stars = 3,
                Text = "text"
            };

            review.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ReviewWithInvalidSurnameFailsValidation()
        {
            Review review = new Review()
            {
                Name = "Pedro",
                Surname = "Perez56",
                Stars = 3,
                Text = "text"
            };

            review.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ReviewWithStarsLowerThanOneFailsValidation()
        {
            Review review = new Review()
            {
                Name = "Pedro",
                Surname = "Perez",
                Stars = 0,
                Text = "text"
            };

            review.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ReviewWithStarsHigherThanFiveFailsValidation()
        {
            Review review = new Review()
            {
                Name = "Pedro",
                Surname = "Perez",
                Stars = 6,
                Text = "text"
            };

            review.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ReviewWithoutTextFailsValidation()
        {
            Review review = new Review()
            {
                Name = "Pedro",
                Surname = "Perez",
                Stars = 6
            };

            review.ValidOrFail();
        }

    }
}
