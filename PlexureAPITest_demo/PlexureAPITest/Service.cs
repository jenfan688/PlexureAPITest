using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PlexureAPITest.Config;
using TechTalk.SpecFlow.Configuration;
using static PlexureAPITest.Config.TestConfig;

namespace PlexureAPITest
{
    public class Service : IDisposable
    {
        HttpClient client;
        ApiConfig apiConfig;

        public Service()
        {
            // Load the configuration from appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();
            apiConfig = configuration.GetSection("ApiConfig").Get<ApiConfig>();

            client = new HttpClient { BaseAddress = new Uri(apiConfig.BaseUrl) };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void UpdateAuthenicationTokenFromRequestHeader(string authToken)
        {
            if (client != null)
            {
                if (client.DefaultRequestHeaders.Contains(TestConfig.RequestHeaders.DefaultAuthenticationToken.HeaderName))
                {
                    client.DefaultRequestHeaders.Remove(TestConfig.RequestHeaders.DefaultAuthenticationToken.HeaderName);
                }

                client.DefaultRequestHeaders.Add(
                    TestConfig.RequestHeaders.DefaultAuthenticationToken.HeaderName,
                    authToken);
            }
        }

                public Response<UserEntity> Login(string username, string password)
        {
            var dict = new Dictionary<String, String>();
            dict.Add("UserName", username);
            dict.Add("Password", password);
            
            string json = JsonConvert.SerializeObject(dict, Formatting.Indented);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            using (var response = client.PostAsync("api/login", httpContent).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    var user = JsonConvert.DeserializeObject<UserEntity>(response.Content.ReadAsStringAsync().Result);

                    client.DefaultRequestHeaders.Remove("token");
                    client.DefaultRequestHeaders.Add("token", user.AccessToken);

                    return new Response<UserEntity>(response.StatusCode, user);
                }

                return new Response<UserEntity>(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public Response<PurchaseEntity> Purchase(int productId)
        {
            var dict = new Dictionary<string, int>();
            dict.Add("ProductId", productId);
            string json = JsonConvert.SerializeObject(dict, Formatting.Indented);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            using (var response = client.PostAsync("api/purchase", httpContent).Result)
            {

                if(response.IsSuccessStatusCode)
                {
                    var purchase = JsonConvert.DeserializeObject<PurchaseEntity>(response.Content.ReadAsStringAsync().Result);

                    return new Response<PurchaseEntity>(response.StatusCode, purchase);
                }

                return new Response<PurchaseEntity>(response.StatusCode, response.Content.ReadAsStringAsync().Result);

            }
        }

        public Response<PointsEntity> GetPoints()
        {
            using (var response = client.GetAsync("api/points").Result)
            {
                if(response.IsSuccessStatusCode)
                {
                    var points = JsonConvert.DeserializeObject<PointsEntity>(response.Content.ReadAsStringAsync().Result);
                    return new Response<PointsEntity>(response.StatusCode, points);
                }

               return new Response<PointsEntity>(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
                client = null;
            }
        }
    }
}
