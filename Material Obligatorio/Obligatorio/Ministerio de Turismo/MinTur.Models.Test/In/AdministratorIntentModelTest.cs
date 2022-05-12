using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinTur.Models.Test.In
{
    [TestClass]
    public class AdministratorIntentModelTest
    {
        [TestMethod]
        public void AdministratorIntentModelCreatedAsExpected()
        {
            AdministratorIntentModel administratorIntentModel = new AdministratorIntentModel()
            {
                Email = "testuser@gmail.com",
                Password = "TestPassword"
            };

            Administrator administrator = administratorIntentModel.ToEntity();

            Assert.IsTrue(administrator.Email == administratorIntentModel.Email);
            Assert.IsTrue(administrator.Password == administratorIntentModel.Password);
        }
    }
}
