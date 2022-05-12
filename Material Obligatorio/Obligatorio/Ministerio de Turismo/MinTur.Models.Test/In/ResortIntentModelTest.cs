using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;
using System.Linq;

namespace MinTur.Models.Test.In
{
    [TestClass]
    public class ResortIntentModelTest
    {
        [TestMethod]
        public void ToEntityReturnsAsExpected()
        {
            ResortIntentModel resortIntentModel = new ResortIntentModel()
            {
                TouristPointId = 1,
                Name = "Hotel piriapolis",
                Stars = 4,
                Address = "Dirección x 3444",
                PhoneNumber = "1235564",
                ReservationMessage = "Default Message",
                Description = "A Description",
                ImagesData = new List<string>(){
                    "test1","test2"
                },
                PricePerNight = 500,
                Available = true,
            };
            Resort resort = resortIntentModel.ToEntity();

            Assert.IsTrue(resortIntentModel.TouristPointId == resort.TouristPointId);
            Assert.IsTrue(resortIntentModel.Name == resort.Name);
            Assert.IsTrue(resortIntentModel.Stars == resort.Stars);
            Assert.IsTrue(resortIntentModel.Address == resort.Address);
            Assert.IsTrue(resortIntentModel.PhoneNumber == resort.PhoneNumber);
            Assert.IsTrue(resortIntentModel.ReservationMessage == resort.ReservationMessage);
            Assert.IsTrue(resortIntentModel.Description == resort.Description);
            Assert.IsTrue(resortIntentModel.PricePerNight == resort.PricePerNight);
            Assert.IsTrue(resortIntentModel.Available == resort.Available); 
            CollectionAssert.AreEquivalent(resortIntentModel.ImagesData, resort.Images.Select(image => image.Data).ToList());
        }
    }
}