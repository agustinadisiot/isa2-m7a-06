using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;
using Moq;
using System;
using System.Linq;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class ResortSearchResultModelTest
    {
        [TestMethod]
        public void ResortSearchResultModelGetsCreatedAsExpected() 
        {
            TouristPoint touristPoint = new TouristPoint() { Id = 1, Name = "Punta del Este" };
            Resort resort = new Resort()
            {
                Id = 3,
                Address = "Direccion",
                Description = "Descripcion ....",
                Name = "Hotel Italiano",
                PricePerNight = 520,
                Stars = 4,
                TouristPoint = touristPoint,
                TouristPointId = touristPoint.Id,
                Punctuation = 3.2
            };

            resort.Images.Add(new Image() { Id = 1 , Data = "Imagen 1" });
            resort.Images.Add(new Image() { Id = 1 , Data = "Imagen 2" });

            ResortSearchResultModel resortSearchResultModel = new ResortSearchResultModel(resort);
            Assert.IsTrue(resort.Id == resortSearchResultModel.Id);
            Assert.IsTrue(resort.Address == resortSearchResultModel.Address);
            Assert.IsTrue(resort.Description == resortSearchResultModel.Description);
            Assert.IsTrue(resort.Name == resortSearchResultModel.Name);
            Assert.IsTrue(resort.PricePerNight == resortSearchResultModel.PricePerNight);
            Assert.IsTrue(resort.Stars == resortSearchResultModel.Stars);
            Assert.IsTrue(resort.TouristPointId == resortSearchResultModel.TouristPointId);
            CollectionAssert.AreEquivalent(resortSearchResultModel.Images.Select(i => i.Data).ToList(), resort.Images.Select(i => i.Data).ToList());
            Assert.IsTrue(resortSearchResultModel.Punctuation == resort.Punctuation);
            Assert.IsTrue(resortSearchResultModel.TotalPrice == 0);
        }

    }
}
