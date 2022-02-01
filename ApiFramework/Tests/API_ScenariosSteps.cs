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
using static ApiFramework.HttpClientHelper;

namespace ApiFramework.Tests
{
    [Binding]
    class API_ScenariosSteps
    {      
        public JsonHelper jsonHelper = new JsonHelper();
        public HttpClientHelper clientHelper = new HttpClientHelper();
        public TestHelper testHelper = new TestHelper();
        public ValidateTheResponse validation = new ValidateTheResponse();
        public ApiResponse apiResponse = new ApiResponse();
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
            apiResponse = clientHelper.getApiReponse(endpoint, requestType,inputParameters);
            jsonResponse = apiResponse.jsonResponse;
            statusCode = apiResponse.statusCode;
            statusMessage = apiResponse.statusMessage;
            responseTime = apiResponse.responseTimeInMilliseconds;
            Hooks.test.Pass("I receive response successfuly.");
        }

        [Then(@"The Satus Code should be '(.*)'")]
        public void ThenTheSatusCodeShouldBe(int StatusCode)
        {
            if (apiResponse.statusCode != StatusCode)
                Hooks.test.Fail("Status Code mismtach. Expected Status Code is: " + StatusCode + " Received Status Code is: " + apiResponse.statusCode);
            else
                Hooks.test.Pass("Status Code: " + StatusCode + " matched successfully.");
        }

        [Then(@"The Response Time should be less than '(.*)' milliseconds")]
        public void ThenTheResponseTimeShouldBeLessThanMiliSeconds(int ResponseTimeInMilliseconds)
        {
            if (apiResponse.responseTimeInMilliseconds > ResponseTimeInMilliseconds)
                Hooks.test.Fail("Response Time mismtach. Expected Response within: " + ResponseTimeInMilliseconds + "  milliseconds.Received Response Time is: " + apiResponse.responseTimeInMilliseconds);
            else
                Hooks.test.Pass("Response Receieved within " + ResponseTimeInMilliseconds + " milliseconds. Response Time  is: " + apiResponse.responseTimeInMilliseconds + " milliseconds");
        }

        [Then(@"The Response should contain text '(.*)'")]
        public void ThenTheResponseShouldContainText(string ExpetedText)
        {
            if (!apiResponse.jsonResponse.Contains(ExpetedText.ToLower()))
                Hooks.test.Fail("API Response does not contains text: " + ExpetedText);
            else
                Hooks.test.Pass("API Response contains text: " + ExpetedText);
        }


        [Then(@"I validate the json response")]
        public void ThenIValidateTheJsonResponse()
        {
            var inputJson = JsonConvert.DeserializeObject<SimpleGetInputClass>(inputParameters);
            validation.verifyJsonResponseWithDatabase(jsonResponse, inputJson);
        }
    }
}
