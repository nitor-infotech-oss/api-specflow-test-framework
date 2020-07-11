using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework
{
    public class HttpClientHelper
    {
        public string response = null;
        public string requestUrl = null;
        System.Diagnostics.Stopwatch timer = new Stopwatch();

        // WebClient client = new WebClient();
        HttpClient client = new HttpClient();

        JsonHelper jsonHelper = new JsonHelper();

        public class ApiResponse
        {
            public int statusCode;
            public string statusMessage;
            public string jsonResponse;
            public int responseTimeInMilliseconds;
        }
      
        public ApiResponse getApiReponse(string endpoint, string methodType, string jsonInput)
        {
            
            ApiResponse apiResponse = new ApiResponse();

            string baseURL = jsonHelper.GetDataByEnvironment("BaseUrl");
            if (baseURL == "BaseUrl")
                baseURL = null;
            requestUrl = baseURL + endpoint;
            if((methodType.ToLower() == "get" || methodType.ToLower() == "delete") && jsonInput != null)
            {
                requestUrl = jsonHelper.BuildRequestURL(requestUrl, jsonInput);
            }
            client.BaseAddress = new Uri(requestUrl);

            try
            {
                switch (methodType.ToLower())
                {
                    case "get":
                        timer.Start();
                        var responseTask = client.GetAsync(client.BaseAddress);
                        responseTask.Wait();
                        timer.Stop();
                        apiResponse = getAllData(responseTask, timer);
                        return apiResponse;

                    case "post":
                        timer.Start();
                        responseTask = client.PostAsync(client.BaseAddress, new StringContent(jsonInput));
                        responseTask.Wait();
                        timer.Stop();
                        apiResponse = getAllData(responseTask, timer);
                        return apiResponse;

                    case "put":
                        timer.Start();
                        responseTask = client.PutAsync(client.BaseAddress, new StringContent(jsonInput));
                        responseTask.Wait();
                        timer.Start();
                        apiResponse = getAllData(responseTask, timer);
                        return apiResponse;

                    case "delete":
                        timer.Start();
                        responseTask = client.DeleteAsync(client.BaseAddress);
                        responseTask.Wait();
                        timer.Stop();
                        apiResponse = getAllData(responseTask, timer);
                        return apiResponse;

                    default:
                        return null;
                }
            }
            catch(WebException ex)
            {
                timer.Stop();
                HttpWebResponse responseEx = (HttpWebResponse)ex.Response;
                apiResponse.statusCode = (int)responseEx.StatusCode;
                apiResponse.statusMessage = Convert.ToString(responseEx.StatusCode);
                apiResponse.jsonResponse = ex.Message;
                apiResponse.responseTimeInMilliseconds = Convert.ToInt32(timer.ElapsedMilliseconds);
                return apiResponse;
            }
            

        }


        public async Task<string> GenerateToken()
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string Uri = jsonHelper.GetDataByEnvironment("Uri"); 
            var token = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, Uri)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", jsonHelper.GetDataByEnvironment("grant_type") },
                    { "client_id", jsonHelper.GetDataByEnvironment("client_id") },
                    { "client_secret", jsonHelper.GetDataByEnvironment("client_secret") },
                    { "resource", jsonHelper.GetDataByEnvironment("resource") } 
                })
            });

            token.EnsureSuccessStatusCode();

            var payload = JObject.Parse(await token.Content.ReadAsStringAsync());
            return payload.Value<string>("access_token");
        }

        public HttpClient GetClientForTokenAuthentication(string token, HttpClient client)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
                         | System.Net.SecurityProtocolType.Tls
                         | System.Net.SecurityProtocolType.Tls11
                         | System.Net.SecurityProtocolType.Tls12;

            client.DefaultRequestHeaders.Add("ContentType", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
            client.DefaultRequestHeaders.Add("upn", jsonHelper.GetDataByEnvironment("upn"));
            return client;
        }

        public HttpClient GetAuthorization(string authorizationType)
        {
            switch (authorizationType)
            {
                case "No Authentication":
                    return client;

                case "Basic Authentication":
                    string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(jsonHelper.GetDataByEnvironment("Basic_Usernanme") + ":" + jsonHelper.GetDataByEnvironment("Basic_Password"))); //username:password
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials);
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
                         | System.Net.SecurityProtocolType.Tls
                         | System.Net.SecurityProtocolType.Tls11
                         | System.Net.SecurityProtocolType.Tls12;
                    return client;

                case "Bearer Authentication":
                    string token = GenerateToken().Result;
                    client = GetClientForTokenAuthentication(token, client);
                    return client;

                default: return client;
            }
        }

        public ApiResponse getAllData(Task<HttpResponseMessage> responseTask, Stopwatch timer)
        {
            ApiResponse response = new ApiResponse();

            response.jsonResponse = responseTask.Result.Content.ReadAsStringAsync().Result;
            response.statusCode = (int)responseTask.Result.StatusCode;
            response.statusMessage = Convert.ToString(responseTask.Result.StatusCode);
            response.responseTimeInMilliseconds = Convert.ToInt32(timer.ElapsedMilliseconds);

            return response;
        }
    }
}
