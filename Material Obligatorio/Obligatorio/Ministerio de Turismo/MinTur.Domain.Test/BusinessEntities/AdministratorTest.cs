using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MinTur.Domain.Test.BusinessEntities
{
    [TestClass]
    public class AdministratorTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void AdministratorWithInvalidEmailFailsValidation() 
        {
            Administrator administrator = new Administrator()
            {
                Email = "invalid-EEmail@@not/email.eu.uy",
                Password = "giqwjd"
            };
            administrator.ValidOrFail();
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void AdministratorWithNoPasswordFailsValidation()
        {
            Administrator administrator = new Administrator()
            {
                Email = "valid@email.com"
            };
            administrator.ValidOrFail();
        }

        [TestMethod]
        public void ValidAdministratorPassesValidation()
        {
            Administrator administrator = new Administrator()
            {
                Email = "valid@email.com",
                Password = "ufihweuifhw"
            };
            administrator.ValidOrFail();
        }

        [TestMethod]
        public void UpdateWithNewAttributesDoesAsExpected() 
        {
            Administrator administrator = new Administrator()
            {
                Id = 5,
                Email = "valid@email.com",
                Password = "ufihweuifhw"
            }; 
            Administrator newAdministrator = new Administrator()
            {
                Id = 9,
                Email = "new@email.com",
                Password = "asdfwefe"
            };
            administrator.Update(newAdministrator);

            Assert.AreEqual(5, administrator.Id);
            Assert.AreEqual(newAdministrator.Email, administrator.Email);
            Assert.AreEqual(newAdministrator.Password, administrator.Password);
        }

    }
}
