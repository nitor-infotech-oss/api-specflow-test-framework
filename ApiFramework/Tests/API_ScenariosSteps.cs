using ApiFramework.TestClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;

namespace ApiFramework.Tests
{
    [Binding]
    class API_ScenariosSteps
    {
        public string inputParameters;
        public JsonHelper jsonHelper = new JsonHelper();
        public WebClientHelper clientHelper = new WebClientHelper();

        [Given(@"I have a json input file")]
        public void GivenIHaveAJsonInputFile(Table table)
        {
            foreach (var row in table.Rows)
            {
                inputParameters = jsonHelper.ReadJsonFile(row["FileName"]);  // reading json file
            }
        }

        [Then(@"I have a simple GET API response")]
        public void GivenIHaveASimpleGETAPIResponse()
        {
            var requestType = "GET";
            var client = new WebClient();
            string baseURL = "http://fakerestapi.azurewebsites.net/api/Activities";
            IDictionary<string, string> jsonInputCSharp = JsonConvert.DeserializeObject<IDictionary<string, string>>(inputParameters);
            string requestURL = jsonHelper.BuildRequestURL(baseURL, jsonInputCSharp);
            string jsonResponse = clientHelper.GetJsonResponse(requestType, baseURL, requestURL, client, inputParameters);
            var jsonToCSharp = JsonConvert.DeserializeObject<JsonOutput>(jsonResponse); // converting Json response to CSharp 

        }

        [Then(@"I get POST API response")]
        public void ThenIGetPOSTAPIResponse()
        {
            var requestType = "POST";
            var client = new WebClient();
            string baseURL = "https://reqres.in/api/users";
            IDictionary<string, string> jsonInputCSharp = JsonConvert.DeserializeObject<IDictionary<string, string>>(inputParameters);
            string requestURL = jsonHelper.BuildRequestURL(baseURL, jsonInputCSharp);
            string jsonResponse = clientHelper.GetJsonResponse(requestType, baseURL, requestURL, client, inputParameters);
            var jsonToCSharp = JsonConvert.DeserializeObject<JsonOutputPostCall>(jsonResponse);
        }

        [Then(@"I get response for API that require token")]
        public void ThenIGetResponseForAPIThatRequireToken()
        {
            var requestType = "GET";
            var client = new WebClient();
            string baseURL = "https://dummy-api-url-for-tokenBasedAuthentication";
            string token = clientHelper.GenerateToken().Result; // generate token
            client = clientHelper.InitialiseWebClientForTokenAuthentication(token);
            IDictionary<string, string> jsonInputCSharp = JsonConvert.DeserializeObject<IDictionary<string, string>>(inputParameters);
            string requestURL = jsonHelper.BuildRequestURL(baseURL, jsonInputCSharp);
            string jsonResponse = clientHelper.GetJsonResponse(requestType, baseURL, requestURL, client, inputParameters);
            var jsonToCSharp = JsonConvert.DeserializeObject<JsonOutput>(jsonResponse);
        }

        [Then(@"I get API response for Basic Authorization")]
        public void ThenIGetAPIResponseForBasicAuthorization()
        {
            var requestType = "GET";
            var client = new WebClient();
            string baseURL = "https://postman-echo.com/basic-auth";
            IDictionary<string, string> jsonInputCSharp = JsonConvert.DeserializeObject<IDictionary<string, string>>(inputParameters);
            string requestURL = jsonHelper.BuildRequestURL(baseURL, jsonInputCSharp);
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["BasicAuthUsernanme"] + ":" + ConfigurationManager.AppSettings["BasicAuthPassword"])); //username:password
            client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            string jsonResponse = clientHelper.GetJsonResponse(requestType, baseURL, requestURL, client, inputParameters);
            var jsonToCSharp = JsonConvert.DeserializeObject<JsonOutputForBasicAuthentication>(jsonResponse);
        }
     
    }
}
