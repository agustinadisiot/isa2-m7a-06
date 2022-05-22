using System.Net;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace MinTur.WebApiControllerSpecFlow.StepDefinitions
{

    [Binding]
    public sealed class ChargingPointStepDefinition
    {
        private readonly  ScenarioContext _context;
        private ChargingPointIntentModel _chargingPointModel = new ChargingPointIntentModel();
    
        
        public ChargingPointStepDefinition(ScenarioContext scenarioContext)
        {
            _context = scenarioContext;
        }
        

        
        [Given(@"the name is ""(.*)""")]
        public void GivenTheNameIs(string name)
        {
            _chargingPointModel.Name = name;
        }

        [Given(@"the description is ""(.*)""")]
        public void GivenTheDescriptionIs(string description)
        {
            _chargingPointModel.Description = description;
        }

        [Given(@"the address is ""(.*)""")]
        public void GivenTheAddressIs(string address)
        {
            _chargingPointModel.Address = address;
        }

        [Given(@"the regionId exists and is (.*)")]
        public void GivenTheRegionIdExistsAndIs(int regionId)
        {
            _chargingPointModel.RegionId = regionId;
        }

        [Given(@"the user is admin")]
        public void GivenTheUserIsAdmin()
        {
       /*     string requestBody = JsonConvert.SerializeObject(new { email = "matias@admin.com", password = "admin" });

            var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5000/api/login/")
            {
                Content = new StringContent(requestBody)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            };

            var client = new HttpClient();
            var responseToken = await client.SendAsync(request).ConfigureAwait(false);
            _token = responseToken.Content.ToString();

            _context.Set(responseToken.Content.ToString, "token");*/
        }

        [When(@"the user selects button to create a charging point")]
        public async void WhenTheUserSelectsButtonToCreateAChargingPoint()
        {

            string requestBody = JsonConvert.SerializeObject(new { name = _chargingPointModel.Name, address = _chargingPointModel.Address, description = _chargingPointModel.Description, regionId = _chargingPointModel.RegionId });

            var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5000/api/chargingPoints")
            {
                Content = new StringContent(requestBody)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json"),
                    }
                }
            };

            var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", _context.Get<String>("token"));
            var response =  await client.SendAsync(request);
            try
            {
                _context.Set(response.StatusCode, "ResponseStatusCode");
            }
            finally
            {
            }
        }
        
        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int statusCode)
        {
           Assert.AreEqual(statusCode, (int)_context.Get<HttpStatusCode>("ResponseStatusCode"));
        }
    }
}
