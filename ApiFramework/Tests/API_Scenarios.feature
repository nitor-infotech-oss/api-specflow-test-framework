Feature: API-Scenarios


@mytag
Scenario: Simple GET Request
	Given I have a 'GET' API 'ActivitiesAPI'
    Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputForSimpleGET.json   |
	Given Authentication Type 'No Authentication'
	Then I receive API response
	Then I validate the json response

Scenario: Simple POST Request
	Given I have a 'POST' API 'UsersAPI'
    Given I have a json input file
	| FileName |
	| TestData\Environment\JsonInputForSimplePOST.json   |
	Given Authentication Type 'No Authentication'
	Then I receive API response

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
	





