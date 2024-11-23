using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Net;
using TechTalk.SpecFlow;

namespace PlexureAPITest
{
    [Binding]
    public class LoginTestStepDefinitions
    {
        private readonly Service _service;
        private Response<UserEntity> _loginResponse;

        public LoginTestStepDefinitions()
        {
            _service = new Service(); 
        }

        private string _username;
        private string _password;

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

        [StepDefinition(@"I POST login")]
        public void WhenIPostLogin()
        {
            _loginResponse = _service.Login(_username, _password);
        }
        [StepDefinition(@"I got the token")]
        public void ThenIGotTheToken()
        {
            Assert.That(_loginResponse.Entity?.AccessToken, Is.Not.Null.And.Not.Empty);
        }

        [StepDefinition(@"I put token as (.*)")]
        public void GivenIPutTokenUse(int p0)
        {
            throw new PendingStepException();
        }

        //Product ID step
        [StepDefinition(@"I have product id (.*)")]
        public void GivenIHaveProductId(int p0)
        {
            throw new PendingStepException();
        }

        // Respond step
        [StepDefinition(@"The respons code should be (.*)")]
        public void ThenTheResponsCodeShouldBe(int statusCode)
        {
            ClassicAssert.AreEqual((HttpStatusCode)statusCode, _loginResponse.StatusCode);
        }
        //Get points
        [StepDefinition(@"I got the purchase points")]
        public void GivenIGotThePurchasePoints()
        {
            throw new PendingStepException();
        }

    }
}
