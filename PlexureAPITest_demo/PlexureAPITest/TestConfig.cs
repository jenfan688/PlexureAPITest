using PlexureAPITest.DataModel;
using System.Collections.Generic;

namespace PlexureAPITest.Config
{
    public class TestConfig
    {
        public static class RequestHeaders
        {
            public static Header JsonContentType = new Header() { HeaderName = "ContentType", HeaderValue = "application/json" };

            // This is the default user authentication token
            public static Header DefaultAuthenticationToken = new Header() { HeaderName = "token", HeaderValue = "37cb9e58-99db-423c-9da5-42d5627614c5" };
        }
        public class ApiConfig
        {
            public string BaseUrl { get; set; }
            public Dictionary<string, string> Headers { get; set; }
        }
    }
}
