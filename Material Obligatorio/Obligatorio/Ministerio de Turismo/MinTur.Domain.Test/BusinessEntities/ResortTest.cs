using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MinTur.Domain.Test.BusinessEntities
{
    [TestClass]
    public class ResortTest
    {
        [TestMethod]
        public void UpdateResortPunctuationDoesAsExpected1()
        {
            Resort resort = new Resort();
            resort.Reviews = new List<Review>()
            {
                new Review() {Stars = 3},
                new Review() {Stars = 5},
                new Review() {Stars = 1}
            };
            Review newReview = new Review() { Stars = 3 };

            resort.UpdateResortPunctuation(newReview);
            Assert.AreEqual(3, resort.Punctuation);
        }

        [TestMethod]
        public void UpdateResortPunctuationDoesAsExpected2()
        {
            Resort resort = new Resort();
            resort.Reviews = new List<Review>()
            {
                new Review() {Stars = 5},
                new Review() {Stars = 1}
            };
            Review newReview = new Review() { Stars = 2 };

            resort.UpdateResortPunctuation(newReview);
            Assert.AreEqual(2.7, resort.Punctuation);
        }

        [TestMethod]
        public void UpdateResortPunctuationDoesAsExpected3()
        {
            Resort resort = new Resort();
            resort.Reviews = new List<Review>()
            {
            };
            Review newReview = new Review() { Stars = 1 };

            resort.UpdateResortPunctuation(newReview);
            Assert.AreEqual(1, resort.Punctuation);
        }

        [TestMethod]
        public void UpdateResortPunctuationDoesAsExpected4()
        {
            Resort resort = new Resort();
            resort.Reviews = new List<Review>()
            {
                new Review() {Stars = 5},
                new Review() {Stars = 1},
                new Review() {Stars = 1},
                new Review() {Stars = 4},
                new Review() {Stars = 2}
            };
            Review newReview = new Review() { Stars = 2 };

            resort.UpdateResortPunctuation(newReview);
            Assert.AreEqual(2.5, resort.Punctuation);
        }

        [TestMethod]
        public void UpdateResortPunctuationDoesAsExpected5()
        {
            Resort resort = new Resort();
            resort.Reviews = new List<Review>()
            {
                new Review() {Stars = 1},
                new Review() {Stars = 2},
            };
            Review newReview = new Review() { Stars = 1 };

            resort.UpdateResortPunctuation(newReview);
            Assert.AreEqual(1.3, resort.Punctuation);
        }

        [TestMethod]
        public void ValidResortPassesValidation()
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Address = "Direccion",
                Description = "Descripcion ....",
                ReservationMessage = "Gracias",
                Name = "Resort",
                PhoneNumber = "09564856",
                PricePerNight = 520,
                Stars = 4,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                Images = new List<Image>()
                {
                    CreateImageWithId(1),
                },
                TouristPointId = 2
            };
            resort.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ResortWithInvalidNameFails()
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Address = "Direccion",
                Description = "Descripcion ....",
                ReservationMessage = "Gracias",
                Name = "988gedwqas-!",
                PhoneNumber = "09564856",
                PricePerNight = 520,
                Stars = 4,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                Images = new List<Image>()
                {
                    CreateImageWithId(1),
                },
                TouristPointId = 2
            };

            resort.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ResortWithInvalidQuantityOfImagesFails()
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Address = "Direccion",
                ReservationMessage = "Gracias",
                Description = "Descripcion ....",
                Name = "Hotel argentino",
                PhoneNumber = "09564856",
                PricePerNight = 520,
                Stars = 4,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                Images = new List<Image>(),
                TouristPointId = 2
            };

            resort.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ResortWithInvalidPricePerNightFails()
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Address = "Direccion",
                Description = "Descripcion ....",
                ReservationMessage = "Gracias",
                Name = "Hotel argentino",
                PhoneNumber = "09564856",
                PricePerNight = -50,
                Stars = 4,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                Images = new List<Image>()
                {
                    CreateImageWithId(1),
                },
                TouristPointId = 2
            };

            resort.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ResortWithInvalidStartsFailsCase1()
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Address = "Direccion",
                Description = "Descripcion ....",
                ReservationMessage = "Gracias",
                PhoneNumber = "09564856",
                Name = "Hotel argentino",
                PricePerNight = 520,
                Stars = 0,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                Images = new List<Image>()
                {
                    CreateImageWithId(1),
                },
                TouristPointId = 2
            };

            resort.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ResortWithInvalidStartsFailsCase2()
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Address = "Direccion",
                Description = "Descripcion ....",
                PhoneNumber = "09564856",
                ReservationMessage = "Gracias",
                Name = "Hotel argentino",
                PricePerNight = 520,
                Stars = 6,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                Images = new List<Image>()
                {
                    CreateImageWithId(1),
                },
                TouristPointId = -1
            };

            resort.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ResortWithoutAddressFailsValidation()
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Description = "Descripcion ....",
                ReservationMessage = "Gracias",
                PhoneNumber = "09564856",
                Name = "Hotel argentino",
                PricePerNight = 520,
                Stars = 5,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                Images = new List<Image>()
                {
                    CreateImageWithId(1),
                },
                TouristPointId = 3
            };

            resort.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ResortWithoutReservationMessageFailsValidation()
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Description = "Descripcion ....",
                PhoneNumber = "09564856",
                Address = "Direccion.. ",
                Name = "Hotel argentino",
                PricePerNight = 520,
                Stars = 5,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                Images = new List<Image>()
                {
                    CreateImageWithId(1),
                },
                TouristPointId = 3
            };

            resort.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ResortWithoutDescriptionFailsValidation()
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Address = "Direccion.. ",
                Name = "Hotel argentino",
                PhoneNumber = "09564856",
                ReservationMessage = "Gracias",
                PricePerNight = 520,
                Stars = 5,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                Images = new List<Image>()
                {
                    CreateImageWithId(1),
                },
                TouristPointId = 3
            };

            resort.ValidOrFail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ResortWithInvalidPhoneNumberFailsValidation()
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Address = "Direccion.. ",
                Description = "Descripcion ....",
                PhoneNumber = "095648ew",
                Name = "Hotel argentino",
                ReservationMessage = "Gracias",
                PricePerNight = 520,
                Stars = 5,
                TouristPoint = new TouristPoint()
                {
                    Id = 2,
                    Name = "Punta del este"
                },
                Images = new List<Image>()
                {
                    CreateImageWithId(1),
                },
                TouristPointId = 3
            };

            resort.ValidOrFail();
        }

        #region Helpers
        private Image CreateImageWithId(int id)
        {
            return new Image()
            {
                Id = id,
                Data = "Test",
            };
        }

        #endregion
    }
}