Feature: ApiScenarios
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Simple GET Request
	Given I have a 'GET' API 'http://fakerestapi.azurewebsites.net/api/Activities'
    Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputForSimpleGET.json   |
	Given Authentication Type 'No Authentication'
	Then I receive API response

Scenario: Simple POST Request
	Given I have a 'POST' API 'https://reqres.in/api/users'
    Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputForSimplePOST.json   |
	Given Authentication Type 'No Authentication'
	Then I receive API response

Scenario: GET Request for Token Authentication
	Given I have a 'GET' API 'https://dummy-api-url-for-tokenBasedAuthentication'
     Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputForTokenGET.json   |
	Given Authentication Type 'Bearer Authentication'
	Then I receive API response

Scenario: GET request with basic authentication username and password
	Given I have a 'GET' API 'https://postman-echo.com/basic-auth'
	Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputBasicAuth.json   |
	Given Authentication Type 'Basic Authentication'
	Then I receive API response
