using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework
{
    public class WebClientHelper
    {
        public string response = null;
        public string requestUrl = null;
        WebClient client = new WebClient();
        JsonHelper jsonHelper = new JsonHelper();

        public string GetResponse(string requestType, string endpoint, string inpuParams)
        {
            try
            {
                string baseURL = jsonHelper.GetDataByEnvironment("BaseUrl");
                if (baseURL == "BaseUrl")
                    baseURL = null;
                requestUrl = baseURL + endpoint;
                if (requestType == "GET") //For GET, DELETE
                {
                    if (inpuParams != null)
                    {
                        requestUrl = jsonHelper.BuildRequestURL(requestUrl, inpuParams);
                    }
                    response = client.DownloadString(requestUrl);
                }
                else // for PUT, POST
                {
                    response = client.UploadString(requestUrl, requestType, inpuParams);                   
                }
                return response;               
            }
            catch (WebException ex)
            {
                HttpWebResponse responseEx = (HttpWebResponse)ex.Response;
                response = (int)responseEx.StatusCode + " " + responseEx.StatusCode.ToString();
                return response;
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

        public WebClient GetClientForTokenAuthentication(string token, WebClient client)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
                         | System.Net.SecurityProtocolType.Tls
                         | System.Net.SecurityProtocolType.Tls11
                         | System.Net.SecurityProtocolType.Tls12;

            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = string.Format("Bearer {0}", token);
            client.Headers["upn"] = ConfigurationManager.AppSettings["upn"];
            return client;
        }

        public WebClient GetAuthorization(string authorizationType)
        {
            switch (authorizationType)
            {
                case "No Authentication":
                    return client;

                case "Basic Authentication":
                    string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(jsonHelper.GetDataByEnvironment("Basic_Usernanme") + ":" + jsonHelper.GetDataByEnvironment("Basic_Password"))); //username:password
                    client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
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
    }
}
