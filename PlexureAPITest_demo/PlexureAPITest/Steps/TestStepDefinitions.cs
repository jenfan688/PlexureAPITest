using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Serilog;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using Gherkin;

namespace PlexureAPITest
{
    [Binding]
    public class TestStepDefinitions
    {
        private readonly Service _service;
        private Response<UserEntity> _loginResponse;
        private Response<PurchaseEntity> _purchaseResponse;
        private Response<PointsEntity> _getPointsResponse;
        private string _username;
        private string _password;
        private string _productId;
        private string _token;

        public TestStepDefinitions()
        {
            _service = new Service(); 
        }

        [StepDefinition(@"I have username (.*)")]
        public void GivenIHaveusername(string username)
        {
            _username = username;
        }

        [StepDefinition(@"I have password (.*)")]
        public void GivenIHavePlexure(string password)
        {
            _password = password;
        }

        [StepDefinition(@"I have product id (.*)")]
        public void GivenIHaveProductId(string productId)
        {
           _productId = productId;
            

        }
        [Given(@"I have token (.*)")]
        public void GivenIHavetoken(string encryedToken)
        {
            _token = encryedToken;
        }

        //API method 
        [StepDefinition(@"I POST (.*)$")]
        public async Task WhenIPOST(string appendix)
        {
            switch (appendix)
            {
                case "login":
                    _loginResponse = await _service.Login(_username, _password);
                    if (_loginResponse == null)
                    {
                        throw new InvalidOperationException("Login response is null.");
                    }
                    break;

                case "purchase":
                    _purchaseResponse = await _service.Purchase(_productId,_token);
                    break;

                default:
                    throw new ArgumentException($"Unknown action: {appendix}");
            }
        }
        [StepDefinition(@"I GET (.*)$")]
        public async Task GivenIGETAsync(string appendix)
        {
            switch (appendix)
            {
                case "points":
                    _getPointsResponse = await _service.GetPoints(_productId, _token);
                    break;
                default:
                    throw new ArgumentException($"Unknown action: {appendix}");
            }
        }


        [StepDefinition(@"I got the token")]
        public void ThenIGotTheToken(string token)
        {
            _token = token;
            Assert.That(_loginResponse.Entity?.AccessToken, Is.Not.Null.And.Not.Empty);
        }

        // Respond steps for login
        [StepDefinition(@"The login respons code should be (.*)")]
        public void LoginResponsCodeShouldBe(int statusCode)
        {
            Console.WriteLine($"Expected: {(HttpStatusCode)statusCode}, Actual: {_loginResponse.StatusCode}");
            if (_loginResponse == null)
            {
                Assert.Fail("Login response is null");
            }
            ClassicAssert.AreEqual((HttpStatusCode)statusCode, _loginResponse.StatusCode);
        }

        [StepDefinition(@"The login respons body should be (.*) except ""([^""]*)""")]
        public Task LoginResponsBodyShouldBeExceptAccessToken(string expectedResponseBodyFile,string jobjectItem)
        {
            var actualResponseBody = _loginResponse.Entity;

            string expectedJson = File.ReadAllText(Path.Combine(@"C:\Users\yujia\workSpace\PlexureAPITest\PlexureAPITest_demo\PlexureAPITest\Responsfiles\", expectedResponseBodyFile));
            var actualResponseJson = JsonConvert.SerializeObject(actualResponseBody, Formatting.Indented);
            var expectedResponseJson = expectedJson;

            actualResponseJson = RemoveAccessToken(actualResponseJson);
            expectedResponseJson = RemoveAccessToken(expectedResponseJson);
            ClassicAssert.AreEqual(expectedResponseJson, actualResponseJson);
            return Task.CompletedTask;
        }
        //Response step for purchase 
       
        [StepDefinition(@"The purchase respons body should be (.*)")]
        public Task PurchaseResponsBodyShouldBe(string expectedResponseBodyFile)
        {
            var actualResponseBody = _purchaseResponse.Entity;

            string expectedJson = File.ReadAllText(Path.Combine(@"C:\Users\yujia\workSpace\PlexureAPITest\PlexureAPITest_demo\PlexureAPITest\Responsfiles\", expectedResponseBodyFile));
            var actualResponseJson = JsonConvert.SerializeObject(actualResponseBody);
            var expectedResponseJson = expectedJson;
            ClassicAssert.AreEqual(expectedResponseJson, actualResponseJson);
            return Task.CompletedTask;
        }

        [StepDefinition(@"The purchase respons code should be (.*)")]
        public void PurchaseResponsCodeShouldBe(int statusCode)
        {
            Console.WriteLine($"Expected: {(HttpStatusCode)statusCode}, Actual: {_purchaseResponse.StatusCode}");
            if (_purchaseResponse == null)
            {
                Assert.Fail("Login response is null");
            }
            ClassicAssert.AreEqual((HttpStatusCode)statusCode, _purchaseResponse.StatusCode);
        }

        [StepDefinition(@"The purchase resonpse key should have values")]
        public void PurchaseResonpseKeyShouldHaveValues(Table table)
        {
            if (_purchaseResponse == null)
            {
                Assert.Fail("Purchase response is null");
            }
            var actualResponseBody = JsonConvert.SerializeObject(_purchaseResponse.Entity, Formatting.Indented);
            var jToken = JToken.Parse(actualResponseBody);
            foreach (var row in table.Rows)
            {
                var key = row["key"];
                var expectedValue = row["value"];
                try
                {
                    var actualValue = jToken.SelectToken(key, true)?.ToString();
                    Log.Information("Key: {Key}, Actual Value: {ActualValue}, Expected Value: {ExpectedValue}",
                          key, actualValue, expectedValue);
                    ClassicAssert.AreEqual(expectedValue, actualValue, $"For key '{key}', expected value '{expectedValue}' but got '{actualValue}'");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error processing key: {Key}", key);
                    Assert.Fail($"Error processing key: {key} - {ex.Message}");
                }
            }
        }
        //Get points API
        [StepDefinition(@"The get points respons code should be (.*)")]
        public void GetPointsResponsCodeShouldBe(int statusCode)
        {
            Console.WriteLine($"Expected: {(HttpStatusCode)statusCode}, Actual: {_getPointsResponse.StatusCode}");
            if (_getPointsResponse == null)
            {
                Assert.Fail("Get points call response is null");
            }
            ClassicAssert.AreEqual((HttpStatusCode)statusCode, _getPointsResponse.StatusCode);
        }

        [StepDefinition(@"The get points resonpse key should have values")]
        public void GetPointsResonpseKeyShouldHaveValues(Table table)
        {
            if (_getPointsResponse == null)
            {
                Assert.Fail("Get points response is null");
            }
            var actualResponseBody = JsonConvert.SerializeObject(_getPointsResponse.Entity, Formatting.Indented);
            var jToken = JToken.Parse(actualResponseBody);
            foreach (var row in table.Rows)
            {
                var key = row["key"];
                var expectedValue = row["value"];
                try
                {
                    var actualValue = jToken.SelectToken(key, true)?.ToString();
                    Log.Information("Key: {Key}, Actual Value: {ActualValue}, Expected Value: {ExpectedValue}",
                          key, actualValue, expectedValue);
                    ClassicAssert.AreEqual(expectedValue, actualValue, $"For key '{key}', expected value '{expectedValue}' but got '{actualValue}'");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error processing key: {Key}", key);
                    Assert.Fail($"Error processing key: {key} - {ex.Message}");
                }
            }
        }

        // Token should be ignored here as it might be expiry
        private string RemoveAccessToken(string jsonResponse)
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(jsonResponse);
            jObject.Property("AccessToken")?.Remove();
            return jObject.ToString(Formatting.Indented);
        }

    }
}
