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
        string response = null;

        public string GetJsonResponse(string requestType, string baseURL, string requestURL, WebClient client,
             string inputParameters)
        {
            try
            {
                if(requestType == "GET")
                {
                    response = client.DownloadString(requestURL);
                }
                else // for PUT, POST, DELETE
                {
                    response = client.UploadString(baseURL, inputParameters);
                }
                Hooks.test.Pass("JSON response received successfully.");
                return response;
            }
            catch (WebException ex)
            {
                switch (((HttpWebResponse)ex.Response).StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        response = "404 Not Found";
                        break;

                    case HttpStatusCode.Unauthorized:
                        response = "401 Unauthorized";
                        break;

                    case HttpStatusCode.BadRequest:
                        response = "400 Bad Request";
                        break;

                    default:
                        throw ex;
                }
                Hooks.test.Fail("Error in receiving JSON response. : " + response);
                return response;
            }
        }

        public async Task<string> GenerateToken()
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string Uri = "https://dummy_app_URL/token";
            var token = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, Uri)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", ConfigurationManager.AppSettings["grant_type"]},
                    { "client_id", ConfigurationManager.AppSettings["client_id"]},
                    { "client_secret", ConfigurationManager.AppSettings["client_secret"]},
                    { "resource", ConfigurationManager.AppSettings["resource"]}
                })
            });

            token.EnsureSuccessStatusCode();

            var payload = JObject.Parse(await token.Content.ReadAsStringAsync());
            return payload.Value<string>("access_token");
        }

        public WebClient InitialiseWebClientForTokenAuthentication(string token)
        {
            var client = new WebClient();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
                         | System.Net.SecurityProtocolType.Tls
                         | System.Net.SecurityProtocolType.Tls11
                         | System.Net.SecurityProtocolType.Tls12;

            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = string.Format("Bearer {0}", token);
            client.Headers["upn"] = ConfigurationManager.AppSettings["upn"];
            return client;
        }


    }
}
