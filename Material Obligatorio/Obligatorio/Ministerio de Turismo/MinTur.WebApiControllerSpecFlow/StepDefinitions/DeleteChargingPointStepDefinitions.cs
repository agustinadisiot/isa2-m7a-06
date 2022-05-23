using System;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace MinTur.WebApiControllerSpecFlow.StepDefinitions
{
    [Binding]
    public class DeleteChargingPointStepDefinitions
    {
        private readonly ScenarioContext _context;
        private int _chargingPointId;


        public DeleteChargingPointStepDefinitions(ScenarioContext scenarioContext)
        {
            _context = scenarioContext;
        }


        [Given(@"the charging point id is (.*)")]
        public void GivenTheChargingPointIdIs(int chargingPointId)
        {
            _chargingPointId = chargingPointId;
        }

        [When(@"the user selects button to delete a charging point")]
        public async void WhenTheUserSelectsButtonToDeleteAChargingPoint()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"http://localhost:5000/api/chargingPoints/{_chargingPointId}"){ };
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", _context.Get<String>("token"));
            var response = await client.SendAsync(request);
            try
            {
                _context.Set(response.StatusCode, "ResponseStatusCode");
            }
            finally
            {
            }
        }
        

        [Then(@"the result code should be (.*)")]
        public void ThenTheResultCodeShouldBe(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)_context.Get<HttpStatusCode>("ResponseStatusCode"));
        }
    }
}
