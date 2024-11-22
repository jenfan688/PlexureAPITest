using System;
using TechTalk.SpecFlow;

namespace PlexureAPITest
{
    [Binding]
    public class LoginTestStepDefinitions
    {
        [StepDefinition(@"I have username (.*)")]
        public void GivenIHaveusername(string username)
        {
            throw new PendingStepException();
        }

        [StepDefinition(@"I have password (.*)")]
        public void GivenIHavePlexure(string password)
        {
            throw new PendingStepException();
        }

        [StepDefinition(@"I post the request")]
        public void GivenIPostTheRequest()
        {
            throw new PendingStepException();
        }
        [StepDefinition(@"I got the token")]
        public void GivenIGotTheToken()
        {
            throw new PendingStepException();
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
        public void ThenTheResponsCodeShouldBe(int p0)
        {
            throw new PendingStepException();
        }
        //Get points
        [StepDefinition(@"I got the purchase points")]
        public void GivenIGotThePurchasePoints()
        {
            throw new PendingStepException();
        }

    }
}
