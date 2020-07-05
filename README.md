REST API AUTOMATION USING BDD FRAMEWORK

Contents:
1.	Purpose of API Automation Framework.
2.	Overview
3.	Advantages
4.	Reusable components
5.	Framework structure
6.	Packages used
7.	Code Optimization
8.	Reporting

Purpose of API Automation Framework:

The purpose for this API automation framework is to serve a technical implementation guideline for automation testing.
With the help for BDD implementation this framework not only helps to reuse the code but also helps to set standard for test scripts.

Overview:

To automate REST API this framework uses SpecFlow which is a testing framework that supports Behaviour Driven Development (BDD). In order to consume web API I have used Web Client class. The Web Client class provides methods to send and receive data from the server. Depending upon the security, an API may or may not require any authentication or authorization. This framework covers the three scenarios with two most commonly user authentication methods.
1.	Consume API which do not have authentication or authorization. (GET and POST requests covered)
2.	Consume API which requires Basic Authentication (Get request covered. POST request process same as first scenario).
3.	Consume API which requires Bearer Authentication/Token Authentication (Get request covered. POST request process same as first scenario).

Advantages:

1.	Supports Html reporting.
2.	Supports execution for testing multiple environments by simply providing required test environment name.
3.	Supports Database connection.
4.	Supports different types for authorization like Basic Authentication and OAuth2 (bearer token).
5.	Can be used for Unit testing by developers.
6.	Use of BDD framework allows every person like Testers, Developers, business analyst, etc., to participate actively.
7.	Enhanced speed at which testing progresses.

Reusable Components:

One of the main goals of this framework is to reduce the number of lines of code require to write a test. This can be achieved by using below methods as required.

1)	GetResponse(string requestType, string endpoint, string inpuParams)

You can receive the API response by simply passing above three parameters to the method.
The response you will receive will be in string format.
   
2)	GetAuthorization(string authorizationType)

This methods currently handles two main authorization types:
1) Basic Authentication.
2) OAuth2 (Bearer Authentication)
The return type of this method is WebClient.

3)	BuildRequestURL(string requestUrl, string jsonInputParams)

This method helps to create the request URL for GET method type API. The input parameters are in Json format. The return type of this method is string.

4)	GenerateToken()

This method helps to generate the Bearer Token required for authentication.
The return type of this method is string.

5)	GetDataByEnvironment(string parameter)

All the config data that is stored in json file are in key value pairs. This config file contains data like endpoints of API, connection string, table names, base url, credentials, etc. By passing the key to above method will return its value in string format.

6)	getSqlQueryResult(string query)

This method returns the SQL result by simply passing the query. The return type of this method is SqlDataReader.

7)	Hooks.cs

This class is helpful for report generation. By simply calling below two methods you can write test case pass or fail condition.
1)	Hooks.test.Pass(“Your pass condition.”)
2)	Hooks.test.Fail(“Your fail condition.”)


Framework structure:
1.	Tests Folder:

All the feature files and step definition files are kept under the Test folder. These tests are described here in a simple English language called as Gherkin language.

2.	Test Class Folder:

This folder contains the class file for the tests which helps us to convert JSON inputs or JSON response to .Net type.

3.	Test Data Folder:

This folder contains various combination of json inputs/params required to provide for an API. These files are in json format. You can provide positive as well as negative inputs to the API.

4.	Reports Folder:

The test results are under this folder. This report is in html format.

5.	 Helper Folder:

The helper folder contains helper classes which intends to give quick implementation of basic methods that can be used again and again.

Packages used: 
Microsoft.AspNet.WebApi.Client, Version: 5.2.7 NUnit, Version: 3.12.0 Newtonsoft.Json, Verison: 12.0.2 Specflow, Version: 2.3.2 ExtentReports, Version: 3.1.3
