Feature: API-Scenarios


@mytag
Scenario: Simple GET Request
	Given I have a 'GET' API 'SimpleGetAPI'
    Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputForSimpleGET.json   |
	Given Authentication Type 'No Authentication'
	Then I receive API response
	Then The Satus Code should be '202'
	Then The Response Time should be less than '1000' milliseconds
	Then The Response should contain text '@reqres.in'
	Then I validate the json response

Scenario: Simple POST Request
	Given I have a 'POST' API 'SimplePostAPI'
    Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputForSimplePOST.json   |
	Given Authentication Type 'No Authentication'
	Then I receive API response
	Then The Satus Code should be '201'
	Then The Response Time should be less than '1000' milliseconds
	Then The Response should contain text 'Created'

Scenario: GET Request for Token Authentication
	Given I have a 'GET' API 'TokenAPI'
     Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputForTokenGET.json   |
	Given Authentication Type 'Bearer Authentication'
	Then I receive API response

Scenario: GET request with basic authentication username and password
	Given I have a 'GET' API 'PostmanAPI'
	Given Authentication Type 'Basic Authentication'
	Then I receive API response
	Then The Satus Code should be '201'
	Then The Response Time should be less than '1000' milliseconds

	





