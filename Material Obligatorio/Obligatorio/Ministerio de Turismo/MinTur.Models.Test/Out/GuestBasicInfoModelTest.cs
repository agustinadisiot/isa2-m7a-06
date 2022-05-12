using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.BusinessEntities;
using MinTur.Models.Out;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class GuestBasicInfoModelTest
    {
        [TestMethod]
        public void GuestBasicInfoModelGetsCreatedAsExpected()
        {
            GuestGroup guestGroup = new GuestGroup()
            {
                GuestGroupType = GuestType.Retired.ToString(),
                Amount = 2
            };
            GuestBasicInfoModel guestBasicInfoModel = new GuestBasicInfoModel(guestGroup);

            Assert.AreEqual(guestGroup.GuestGroupType, guestBasicInfoModel.GuestType);
            Assert.AreEqual(guestGroup.Amount, guestBasicInfoModel.Ammount);
        }
    }
}
