REST API AUTOMATION USING BDD FRAMEWORK

1) Overview
2) Framework structure
3) Packages used
4) Code Optimization 
5) Reporting
  
Overview:
To automate REST API this framework uses SpecFlow which is a testing framework that supports Behavior Driven Development (BDD). In order to consume web API I have used Web Client class. The Web Client class provides methods to send and receive data from the server. Depending upon the security, an API may or may not require any authentication or authorization. This framework covers the three scenarios with two most commonly user authentication methods.
1)     Consume API which do not have authentication or authorization. (GET and POST requests covered)
2)     Consume API which requires Basic Authentication (Get request covered. POST request process same as first scenario).
3)     Consume API which requires Bearer Authentication/Token Authentication (Get request covered. POST request process same as first scenario).
 
Framework structure:

1)     Tests Folder
All the feature files and step definition files are kept under the Test folder.
These tests are described here in a simple English language called as Gherkin language.
 
2)     Test Class Folder
This folder contains the class file for the tests which helps us to convert JSON inputs or JSON response to .Net type.
 
3)     Test Data Folder
This folder contains various combination of json inputs/params required to provide for an API. These files are in json format. You can provide positive as well as negative inputs to the API.
 
4)     Reports Folder
The test results are under this folder. This report is in html format.

5)      Helper Folder
The helper folder contains helper classes which intends to give quick implementation of basic methods that can be used again and again.

Packages used:
Microsoft.AspNet.WebApi.Client, Version: 5.2.7
NUnit, Version: 3.12.0
Newtonsoft.Json, Verison: 12.0.2
Specflow, Version: 2.3.2
ExtentReports, Version: 3.1.3


Code Optimization:
One of the main goals of this framework is to reduce the number of lines of code require to write a test. This BuildRequestURL code shall help to build request URL for any API with input params.  

        public string BuildRequestURL(string baseURL, IDictionary<string, string> jsonInputParams)
        {
            if (jsonInputParams.Count == 0)
            {
                return baseURL;
            }
            else
            {               
                List<string> stringValues = new List<string>();
                foreach (var item in jsonInputParams)
                {
                    if (!string.IsNullOrWhiteSpace(item.Value))
                    {
                        stringValues.Add(item.Key + "=" + item.Value);
                    }
                }
                return string.Format("{0}?{1}", baseURL, string.Join("&", stringValues));
            }
        }

  
Reporting:
For reporting I have used Extent Reports package which help to create rich interactive detailed report of your tests. This report will give the test execution status as pass, fail or skip along with execution date and time.

