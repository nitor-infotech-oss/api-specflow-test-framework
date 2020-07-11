using ApiFramework.APIs.BaiscAuthenticationAPI;
using ApiFramework.APIs.SimpleGetAPI;
using ApiFramework.Helper;
using ApiFramework.TestClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
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
        public JsonHelper jsonHelper = new JsonHelper();
        public HttpClientHelper clientHelper = new HttpClientHelper();
        public TestHelper testHelper = new TestHelper();
        public ValidateTheResponse validation = new ValidateTheResponse();
        public string inputParameters;
        public string requestType;
        public string endpoint;
        public string jsonResponse;
        public int statusCode;
        public string statusMessage;
        public int responseTime;

        [Given(@"I have a '(.*)' API '(.*)'")]
        public void GivenIHaveAAPI(string httpVerb, string API)
        {
            requestType = httpVerb;
            endpoint = jsonHelper.GetDataByEnvironment(API);
        }

        [Given(@"I have a json input file")]
        public void GivenIHaveAJsonInputFile(Table table)
        {
            inputParameters = jsonHelper.ReadJsonFile(table);  // Passing table
        }

        [Given(@"I have a json input file '(.*)'")]
        public void GivenIHaveAJsonInputFile(string filePath)
        {
            inputParameters = jsonHelper.ReadJsonFile(filePath); 
        }

        [Given(@"Authentication Type '(.*)'")]
        public void GivenAuthenticationType(string authenticationType)
        {
            clientHelper.GetAuthorization(authenticationType); // call get Authorization method.
        }

        [Then(@"I receive API response")]
        public void ThenIReceiveAPIResponse()
        {
            var apiResponse = clientHelper.getApiReponse(endpoint, requestType,inputParameters);
            jsonResponse = apiResponse.jsonResponse;
            statusCode = apiResponse.statusCode;
            statusMessage = apiResponse.statusMessage;
            responseTime = apiResponse.responseTimeInMilliseconds;
            Hooks.test.Pass("I receive response successfuly.");
        }

        [Then(@"I validate the json response")]
        public void ThenIValidateTheJsonResponse()
        {
            var inputJson = JsonConvert.DeserializeObject<SimpleGetInputClass>(inputParameters);
            validation.verifyJsonResponseWithDatabase(jsonResponse, inputJson);
        }


    }
}
