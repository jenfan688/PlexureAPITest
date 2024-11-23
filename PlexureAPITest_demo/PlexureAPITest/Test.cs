using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PlexureAPITest.Config;

namespace PlexureAPITest
{
    [TestFixture]
    public class Test
    {
        Service service;

        [OneTimeSetUp]
        public void Setup()
        {
            service = new Service();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (service != null)
            {
                service.Dispose();
                service = null;
            }
        }


        //Example test cases:
        //Candidate can add as many test cases as you can. 
        //You can also add some comments if need. 

        [Test]
        public void TEST_001_Login_With_Valid_User()
        {
            var response = service.Login("Tester", "Plexure123").Result;

            ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //NOTE: Please rerun this test to wakeup service if you got 500 server errors
        }

        [Test]
        public void TEST_002_Get_Points_For_Logged_In_User()
        {
            string productId = "1";
            string token = "37cb9e58-99db-423c-9da5-42d5627614c5";
           var response = service.GetPoints(productId,token).Result;
            ClassicAssert.AreEqual(HttpStatusCode.Accepted, response.StatusCode); // 202

            var points = response.Entity;
            ClassicAssert.AreEqual(1, points.UserId);

            //NOTE: The points might get updated by different purchases on the same user,
            //      so we only test if the points > zero in this test.
           ClassicAssert.Greater(points.Value, 0);
        }
    }
        
}
