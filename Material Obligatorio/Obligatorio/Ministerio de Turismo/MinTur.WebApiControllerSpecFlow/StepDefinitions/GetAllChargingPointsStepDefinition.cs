using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;
using TechTalk.SpecFlow;

namespace MinTur.WebApiControllerSpecFlow.StepDefinitions
{
    [Binding]
    public class GetAllChargingPointsStepDefinition
    {
        private readonly  ScenarioContext _context;
        private List<ChargingPointBasicInfoModel>   _chargingPointModels = new List<ChargingPointBasicInfoModel>();
        private bool _existCharginPoints;
        public GetAllChargingPointsStepDefinition(ScenarioContext scenarioContext)
        {
            _context = scenarioContext;
            _existCharginPoints = false;
        }
        
        [Given(@"existing charging points")]
        public void GivenExistingChargingPoints()
        {
            _existCharginPoints = true;
        }
        
        [When(@"the user selects button to get all charging points in the lateral menu")]
        public async void WhenTheUserSelectsButtonToGetAllChargingPointsInTheLateralMenu()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5000/api/chargingPoints/"){ };
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", _context.Get<String>("token"));
            var response = await client.SendAsync(request);
            Console.WriteLine(response.Content);
            Console.ReadLine();
            try
            {
                _context.Set(response.StatusCode, "ResponseStatusCode");
                _context.Set(response.Content, "List");
            }
            finally
            {
            }
        }
        
        [Then(@"a list of charging points should be returned")]
        public void ThenAListOfChargingPointsShouldBeReturned()
        {
           var list =  _context.Get<HttpContent>("List");
           Assert.IsNotNull(list);
        }


        [Given(@"there are not charging points")]
        public void GivenThereAreNotChargingPoints()
        {
            _existCharginPoints = false;
        }

    
        [Then(@"an empty list of charging points should be returned")]
        public void ThenAnEmptyListOfChargingPointsShouldBeReturned()
        {
            var list = _context.Get<HttpContent>("List");
            Assert.IsNull(list);
        }
    }
}
