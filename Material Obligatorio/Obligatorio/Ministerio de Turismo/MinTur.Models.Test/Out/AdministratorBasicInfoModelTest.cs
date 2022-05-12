using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class AdministratorBasicInfoModelTest
    {
        [TestMethod]
        public void AdministratorBasicInfoModelCreatedAsExpected()
        {
            Administrator administrator = new Administrator { Id = 5, Email = "testuser@gmail.com", Password = "TestPassword" };
            AdministratorBasicInfoModel administratorBasicInfoModel = new AdministratorBasicInfoModel(administrator);

            Assert.IsTrue(administrator.Id == administratorBasicInfoModel.Id);
            Assert.IsTrue(administrator.Email == administratorBasicInfoModel.Email);
        }
    }
}
