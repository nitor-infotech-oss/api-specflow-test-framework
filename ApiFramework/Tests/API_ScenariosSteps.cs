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
        public WebClientHelper clientHelper = new WebClientHelper();
        public TestHelper testHelper = new TestHelper();
        public ValidateTheResponse validation = new ValidateTheResponse();
        public string inputParameters;
        public string requestType;
        public string baseUrl;
        public string response;

        [Given(@"I have a '(.*)' API '(.*)'")]
        public void GivenIHaveAAPI(string httpVerb, string API)
        {
            requestType = httpVerb;
            baseUrl = jsonHelper.GetDataByEnvironment(API);
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
            response = clientHelper.GetResponse(requestType, baseUrl, inputParameters);
        }

        [Then(@"I expect status code '(.*)'")]
        public void ThenIExpectStatusCode(int expectedStatusCode)
        {
            if (testHelper.verifyApiResponseStatusCode("404", response) == false)
                Hooks.test.Fail("Response Status Code Mismatch."); // can use Assert.Fail("Response Status Code Mismatch");
            else
                Hooks.test.Pass("Response Statuc Code Matched Successfully.");

        }

        [Then(@"I validate the json response")]
        public void ThenIValidateTheJsonResponse()
        {
            var inputJson = JsonConvert.DeserializeObject<SimpleGetInputClass>(inputParameters);
            validation.verifyJsonResponseWithDatabase(response, inputJson);
        }


    }
}
