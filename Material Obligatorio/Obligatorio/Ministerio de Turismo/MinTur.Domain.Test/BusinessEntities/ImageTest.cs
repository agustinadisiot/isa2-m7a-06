using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinTur.Domain.Test.BusinessEntities
{
    [TestClass]
    public class ImageTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ImageWithoutDataFailsValidation() 
        {
            Image image = new Image();
            image.ValidOrFail();
        }

        [TestMethod]
        public void ValidImagePassesValidation()
        {
            Image image = new Image() { Data = "fhewuihgfuiwe" };
            image.ValidOrFail();
        }
    }
}
