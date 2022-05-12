using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;
using System.Linq;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class ResortDetailsModelTest
    {
        [TestMethod]
        public void ResortDetailsModelGetsCreatedAsExpected() 
        {
            Resort resort = new Resort()
            {
                Id = 3,
                Address = "Direccion",
                Description = "Descripcion ....",
                Name = "Hotel Italiano",
                PricePerNight = 520,
                Stars = 4,
                TouristPointId = 5,
                Available = false,
                Punctuation = 3.6
            };
            resort.Images.Add(new Image() { Id = 2, Data = "uifheruihgfre" });
            resort.Reviews.Add(new Review() { Id = 1, Name = "Pepe", Stars = 3, Surname = "Perez", Text = "Muy cool" });
            resort.Reviews.Add(new Review() { Id = 3, Name = "Agua", Stars = 1, Surname = "Fiestas", Text = "Nah muy mal" });
            ResortDetailsModel resortDetailsModel = new ResortDetailsModel(resort);


            Assert.IsTrue(resort.Id == resortDetailsModel.Id);
            Assert.IsTrue(resort.Address == resortDetailsModel.Address);
            Assert.IsTrue(resort.Description == resortDetailsModel.Description);
            Assert.IsTrue(resort.Name == resortDetailsModel.Name);
            Assert.IsTrue(resort.PricePerNight == resortDetailsModel.PricePerNight);
            Assert.IsTrue(resort.Stars == resortDetailsModel.Stars);
            Assert.IsTrue(resort.TouristPointId == resortDetailsModel.TouristPointId);
            Assert.IsTrue(resort.Punctuation == resortDetailsModel.Punctuation);
            CollectionAssert.AreEquivalent(resortDetailsModel.Images.Select(i => i.Data).ToList(), resort.Images.Select(i => i.Data).ToList());
            CollectionAssert.AreEquivalent(resortDetailsModel.Reviews, resort.Reviews.Select(r => new ReviewDetailsModel(r)).ToList());
        }
    }
}
