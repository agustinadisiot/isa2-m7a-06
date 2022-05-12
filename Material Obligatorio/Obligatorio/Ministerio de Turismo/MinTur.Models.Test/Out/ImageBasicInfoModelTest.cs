using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class ImageBasicInfoModelTest
    {
        [TestMethod]
        public void ImageBasicInfoModelGetsCreatedAsExpected() 
        {
            Image image = new Image()
            {
                Id = 32,
                Data = "dhsaguihger"
            };
            ImageBasicInfoModel imageBasicInfoModel = new ImageBasicInfoModel(image);

            Assert.IsTrue(image.Data == imageBasicInfoModel.Data);
        }
    }
}
