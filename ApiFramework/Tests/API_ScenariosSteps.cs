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
        public string requestType;
        public string baseUrl;
        public string response;

        [Given(@"I have a '(.*)' API '(.*)'")]
        public void GivenIHaveAAPI(string httpVerb, string apiUrl)
        {
            requestType = httpVerb;
            baseUrl = apiUrl;
        }

        [Given(@"I have a json input file")]
        public void GivenIHaveAJsonInputFile(Table table)
        {
            inputParameters = jsonHelper.ReadJsonFile(table);  // Passing table
        }

        [Given(@"I have a json input file '(.*)'")]
        public void GivenIHaveAJsonInputFile(string filePath)
        {
            inputParameters = jsonHelper.ReadJsonFile(filePath); // Passing sting (file Path) 
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

    }
}
