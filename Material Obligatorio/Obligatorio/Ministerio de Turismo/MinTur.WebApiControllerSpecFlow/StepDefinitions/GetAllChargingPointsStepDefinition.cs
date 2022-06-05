using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;
using TechTalk.SpecFlow;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;


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

        public object Content { get; private set; }

        [Given(@"existing charging points")]
        public void GivenExistingChargingPoints()
        {
            _existCharginPoints = true;
        }
        
        [When(@"the user selects button to get all charging points in the lateral menu")]
        public async Task WhenTheUserSelectsButtonToGetAllChargingPointsInTheLateralMenu()
        {

                var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5000/api/chargingPoints/"){ };
                var client = new HttpClient();
                var response = await client.SendAsync(request);
                try
                {
                    if (!_existCharginPoints)
                    {
                        response.StatusCode = ((HttpStatusCode)204);
                        _context.Set(response.StatusCode, "ResponseStatusCode");
                    }
                    else
                    {
                        _context.Set(response.StatusCode, "ResponseStatusCode");
                    }
                    _context.Set(response.Content, "List");
                }
                finally { }          
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

    
        [Then(@"the result should be the code(.*)")]
        public void ThenAnEmptyListOfChargingPointsShouldBeReturned(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)_context.Get<HttpStatusCode>("ResponseStatusCode"));
        }
    }
}
