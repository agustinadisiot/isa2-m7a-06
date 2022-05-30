using System;
using TechTalk.SpecFlow;

namespace MinTur.WebApiControllerSpecFlow.StepDefinitions
{
    [Binding]
    public class GetAllChargingPointsStepDefinition
    {
        [Given(@"there are not charging points")]
        public void GivenThereAreNotChargingPoints()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"the user selects button to get all charging points in the lateral menu")]
        public void WhenTheUserSelectsButtonToGetAllChargingPointsInTheLateralMenu()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
