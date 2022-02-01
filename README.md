REST API AUTOMATION USING BDD FRAMEWORK
Purpose of API Automation Framework:
The purpose for this API automation framework is to serve a technical implementation guideline for automation testing. With the help for BDD implementation this framework not only helps to reuse the code but also helps to set standard for test scripts.

Overview:
To automate REST API this framework uses SpecFlow which is a testing framework that supports Behaviour Driven Development (BDD). In order to consume web API I have used Web Client class. The Web Client class provides methods to send and receive data from the server. Depending upon the security, an API may or may not require any authentication or authorization. This framework covers the three scenarios with two most commonly user authentication methods.
1.	Consume API which do not have authentication or authorization. (GET and POST requests covered)
2.	Consume API which requires Basic Authentication (Get request covered. POST request process same as first scenario).
3.	Consume API which requires Bearer Authentication/Token Authentication (Get request covered. POST request process same as first scenario).

Framework structure:
1.Tests Folder:
 All the feature files and step definition files are kept under the Test folder. These tests are described here in a simple English language called as Gherkin language.
2.Test Class Folder:
This folder contains the class file for the tests which helps us to convert JSON inputs or JSON response to .Net type.
3.Test Data Folder:
 This folder contains various combination of json inputs/params required to provide for an API. These files are in json format. You can provide positive as well as negative inputs to the API.
4.Reports Folder:
The test results are under this folder. This report is in html format.
5.Helper Folder:
The helper folder contains helper classes which intends to give quick implementation of basic methods that can be used again and again.

Packages used: 
Microsoft.AspNet.WebApi.Client, Version: 5.2.7 NUnit, Version: 3.12.0 Newtonsoft.Json, Verison: 12.0.2 Specflow, Version: 2.3.2 ExtentReports, Version: 3.1.3

Reporting:
The test execution report is generated in Html format. This report allows to filter tests according to pass/ fail criteria.


Advantages:
Supports Html reporting.
Supports execution for testing multiple environments by simply providing required test environment name.
Supports Database connection.
Supports different types for authorization like Basic Authentication and OAuth2 (bearer token).
Can be used for Unit testing by developers.
Use of BDD framework allows every person like Testers, Developers, business analyst, etc., to participate actively.
Enhanced speed at which testing progresses.

Limitation:
Supports only REST APIs. Does not support SOAP APIs.

Where and how to use:
This framework can support any project that requires Rest API automation testing in C#.
To use this framework you need to perform following steps:
1.	Install Visual Studio (VS 2017 ,2019 recommended)
2.	Install supporting packages.
3.	Update the API_Data_Config.json file with your project specific API base URLs and endpoints.
4.	Update the app.config by simply entering the environment name to execute all the tests for that environment.
5.	Segregate the test data as per the test environment. 
6.	Test data should be in .json format.
7.	Follow the specflow feature file scenario standards (scenario standards can also be customized).



